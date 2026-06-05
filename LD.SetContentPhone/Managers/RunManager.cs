using LD.SetContentPhone.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LD.SetContentPhone.Managers
{
    public class RunManager
    {
        public event Action<string>? ShowMsg;
        public event Action? Completed;

        private Dictionary<string, AbstractSendManager> _dic = new Dictionary<string, AbstractSendManager>();

        // 中心号码对象
        private ConcurrentQueue<string> _dataQueue = new ConcurrentQueue<string>();
        public RunManager()
        {
        }

        // 控制
        private CancellationTokenSource? _cts;
        private ManualResetEventSlim _pauseEvent = new ManualResetEventSlim(true);
        // 状态
        public bool IsRunning { get; private set; } = false;
        public int ConsumedCount => _consumedCount;
        private int _consumedCount = 0;
        private int _sendPhoneIndex = 0;

        /// <summary>
        /// 设置执行对象
        /// </summary>
        /// <param name="dic"></param>
        public void SetDicCom(Dictionary<string, AbstractSendManager> dic)
        {
            _dic = dic;
        }

        public void ResetDataQueue(IEnumerable<string> lists)
        {
            _dataQueue.Clear();
            _consumedCount = 0;
            _sendPhoneIndex = 0;
            AddDataQueue(lists);
        }

        /// <summary>
        /// 持续添加队列数据
        /// </summary>
        /// <param name="lists"></param>
        public void AddDataQueue(IEnumerable<string> lists)
        {
            foreach (var item in lists)
                _dataQueue.Enqueue(item);
        }

        /// <summary>
        /// 获取未执行的号码
        /// </summary>
        /// <returns></returns>
        public List<string> GetNotExecPhones()
        {
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                ShowMsg?.Invoke("没有停止发送。");
                return new List<string>();
            }

            var list = _dataQueue.ToList();
            _dataQueue.Clear();
            return list;
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            if (IsRunning) return;

            if (_dataQueue.IsEmpty)
            {
                ShowMsg?.Invoke("请先导入中心号码。");
                return;
            }

            if (_dic.Count == 0)
            {
                ShowMsg?.Invoke("没有检测到串口。");
                return;
            }

            _cts = new CancellationTokenSource();
            _pauseEvent.Set();
            IsRunning = true;

            LoggerManager.WriteLog($"任务开始 串口数量:{_dic.Count}");
            StartLoopMode();
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            LoggerManager.WriteLog("任务暂停");
            _pauseEvent.Reset();
        }

        /// <summary>
        /// 继续
        /// </summary>
        public void Resume()
        {
            LoggerManager.WriteLog("任务继续");
            _pauseEvent.Set();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            LoggerManager.WriteLog("任务停止");
            _cts?.Cancel();
            IsRunning = false;
            _pauseEvent.Set();
        }

        public void TryOpen(string key)
        {
            if (_dic.TryGetValue(key, out var manager))
            {
                lock (manager.LockObj)
                {
                    manager.TryOpen();
                }
            }
        }

        public void CloseCom(string key)
        {
            if (_dic.TryGetValue(key, out var manager))
            {
                manager.Close();
            }
        }

        /// <summary>
        /// 模式1：单线程循环消费（轮询各个 SerialPortManager）
        /// </summary>
        private void StartLoopMode()
        {
            CancellationToken token = _cts!.Token;

            _ = Task.Run(async () =>
            {
                try
                {
                    while (!token.IsCancellationRequested && _dataQueue.TryDequeue(out string? centerPhone))
                    {
                        if (string.IsNullOrWhiteSpace(centerPhone))
                        {
                            continue;
                        }

                        _pauseEvent.Wait(token);

                        var managers = GetCarrierManagers();

                        if (managers.Count == 0)
                        {
                            ShowMsg?.Invoke("没有检测到有运营商的串口。");
                            LoggerManager.WriteLog($"中心号:{centerPhone} 没有检测到有运营商的串口，任务停止");
                            _dataQueue.Enqueue(centerPhone);
                            Stop();
                            return;
                        }

                        LoggerManager.WriteLog($"中心号:{centerPhone} 开始并行处理 串口数量:{managers.Count}");

                        var tasks = managers.Select(manager =>
                            Task.Run(async () =>
                            {
                                _pauseEvent.Wait(token);
                                token.ThrowIfCancellationRequested();

                                await SetCenterAndSendAsync(manager, centerPhone, token);
                            }, token));

                        await Task.WhenAll(tasks);
                        LoggerManager.WriteLog($"中心号:{centerPhone} 并行处理完成");
                    }
                }
                catch (OperationCanceledException) { }
                finally
                {
                    IsRunning = false;

                    if (!token.IsCancellationRequested && _dataQueue.IsEmpty)
                    {
                        Completed?.Invoke();
                    }
                }
            }, token);
        }

        private List<AbstractSendManager> GetCarrierManagers()
        {
            return _dic.Values
                .Where(manager => HasCarrier(manager.Carrier))
                .ToList();
        }

        private bool HasCarrier(string carrier)
        {
            if (string.IsNullOrWhiteSpace(carrier))
            {
                return false;
            }

            string value = carrier.Trim();
            return value != "无" && value != "未知" && value != "-";
        }

        private async Task SetCenterAndSendAsync(AbstractSendManager manager, string centerPhone, CancellationToken token)
        {
            string phone = GetSendPhone();
            if (string.IsNullOrEmpty(phone))
            {
                ShowMsg?.Invoke("发送手机号为空，请先保存配置。");
                Stop();
                return;
            }

            string content = BuildSendContent(SmsConfig.ConfigModel.SendContent, centerPhone);
            string result;

            try
            {
                LoggerManager.WriteLog($"COM:{manager.ComName} 手机号:{phone} 设置中心号:{centerPhone}");
                result = await manager.SetCSCA(centerPhone);
                LoggerManager.WriteLog($"COM:{manager.ComName} 手机号:{phone} 设置中心号:{centerPhone} 结果:{result}");
            }
            catch (Exception ex)
            {
                LoggerManager.WriteError($"COM:{manager.ComName} 手机号:{phone} 设置中心号:{centerPhone} 异常", ex);
                manager.RecordSendFail(phone);
                return;
            }

            token.ThrowIfCancellationRequested();

            if (result == "OK")
            {
                try
                {
                    LoggerManager.WriteLog($"COM:{manager.ComName} 手机号:{phone} 发送短信内容:{content}");
                    string sendResult = await manager.SendSmsByPhoneAsync(phone, content, token);
                    LoggerManager.WriteLog($"COM:{manager.ComName} 手机号:{phone} 发送短信内容:{content} 结果:{sendResult}");
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    LoggerManager.WriteError($"COM:{manager.ComName} 手机号:{phone} 发送短信内容:{content} 异常", ex);
                    manager.RecordSendFail(phone);
                }

                Interlocked.Increment(ref _consumedCount);
            }
            else
            {
                manager.RecordSendFail(phone);
            }
        }

        private string BuildSendContent(string content, string centerPhone)
        {
            return $"{content}{centerPhone}";
        }

        private string GetSendPhone()
        {
            if (SmsConfig.SendPhones.Count == 1)
            {
                return SmsConfig.SendPhones[0];
            }

            int next = Interlocked.Increment(ref _sendPhoneIndex);
            int index = (int)((uint)next % (uint)SmsConfig.SendPhones.Count);
            return SmsConfig.SendPhones[index];
        }
    }
}
