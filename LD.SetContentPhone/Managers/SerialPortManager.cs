using LD.SetContentPhone.Models;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD.SetContentPhone.Managers
{
    public class SerialPortManager : AbstractSendManager
    {
        /// <summary>
        /// 模式设置状态
        /// </summary>
        public bool ModeStatus { get; set; } = false;

        public bool IsConnect { get; set; } = false;

        SerialPort? port;
        int baudRate = 0;
        private readonly object _sendResultLock = new object();

        public SerialPortManager(string portName, int baudRate)
        {
            ComName = portName;
            this.baudRate = baudRate;

            InitPort();
        }

        public override void SetMode()
        {
            if (!VerifyConn())
            {
                LoggerManager.WriteLog($"COM:{ComName} 设置短信模式失败:串口未打开");
                return;
            }

            // 一步步执行 AT 指令
            SendWriteLine("AT+CMGF=1\r"); // 文本模式
            Thread.Sleep(10);
            SendWriteLine("AT+CSCS=\"UCS2\"\r"); // UCS2 编码
            Thread.Sleep(10);
            SendWriteLine("AT+CSMP=17,167,0,8\r"); // UCS2 短信参数
            Thread.Sleep(10);
        }

        public override void SendSmsByPhone(string phone, string message)
        {
            InternalSendSmsByPhone(phone, message);
            this.CurrentSendPhone = phone;
            this.SendCount++;
        }

        public async override Task<string> SendSmsByPhoneAsync(string phone, string message, CancellationToken cancellationToken = default)
        {
            if (!VerifyConn())
            {
                RecordSendFail(phone);
                return "ERROR";
            }

            lock (_sendResultLock)
            {
                SendSmsTcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
                RunStatus = EnumRunStatus.SendMsg;
                SendSmsByPhone(phone, message);
            }

            Task<string> sendTask = SendSmsTcs.Task;
            Task delayTask = Task.Delay(10000, cancellationToken);
            Task completed = await Task.WhenAny(sendTask, delayTask);

            if (completed != sendTask)
            {
                lock (_sendResultLock)
                {
                    SendSmsTcs = null;
                    RunStatus = EnumRunStatus.None;
                }

                SendFailCount++;
                return "ERROR";
            }

            string result = await sendTask;
            SendSmsTcs = null;
            return result;
        }

        private void InternalSendSmsByPhone(string phone, string message)
        {
            if (!VerifyConn())
            {
                return;
            }

            // todo 清空缓存会不会导致mode失效
            Discard();

            string phoneUcs2 = ConvertManager.ToUCS2Hex(phone);
            var msgUcs2 = ConvertManager.ToUCS2Hex(message);

            // 输入号码
            SendWriteLine($"AT+CMGS=\"{phoneUcs2}\"\r");
            Thread.Sleep(20); //等待响应

            // 发送短信内容 + Ctrl+Z 结束
            SendWrite(msgUcs2);
            SendWrite(new byte[] { 0x1A }, 0, 1);
        }

        private void InitPort()
        {
            port = new SerialPort(ComName, baudRate, Parity.None, 8, StopBits.One);
            port.ReadTimeout = 3000;
            port.WriteTimeout = 3000;
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
            TryOpen();
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (port == null)
            {
                return;
            }

            string data = port.ReadExisting();
            LoggerManager.WriteLog($"COM:{ComName} AT返回:{data.Replace("\r", "\\r").Replace("\n", "\\n")}");

            // 中心号码
            if (SetCenterPhoneTcs != null && (data.Contains("CSCA") || data.Contains("OK") || data.Contains("ERROR")))
            {
                if (data.Contains("OK") && !data.Contains("ERROR"))
                {
                    SetCenterPhoneTcs.TrySetResult("OK");
                }
                else if (data.Contains("ERROR"))
                {
                    SetCenterPhoneTcs.TrySetResult("ERROR");
                }
            }

            // 信号判断
            if (data.Contains("CREG"))
            {
                if (data.Contains("0,1"))
                {
                    SendStatus = EnumSendStatus.Success;
                }
                else
                {
                    SendStatus = EnumSendStatus.Error;
                }
            }

            // 运营商判断
            if (data.Contains("COPS"))
            {
                if (data.Contains("CHN-CT"))
                {
                    Carrier = "电信";
                }
                else if (data.Contains("CHN-UNICOM"))
                {
                    Carrier = "联通";
                }
                else if (data.Contains("CHINA MOBILE") || data.Contains("CMCC"))
                {
                    Carrier = "移动";
                }
                else
                {
                    Carrier = "无";
                }
            }

            // 短信成功判断
            if (RunStatus == EnumRunStatus.SendMsg)
            {
                if (data.Contains("OK"))
                {
                    SendSuccessCount += 1;
                    SendSmsTcs?.TrySetResult("OK");
                }
                else if (data.Contains("ERROR"))
                {
                    SendFailCount += 1;
                    SendSmsTcs?.TrySetResult("ERROR");
                }
                RunStatus = EnumRunStatus.None;
            }
        }

        public override void TryOpen()
        {
            try
            {
                if (port != null && port.IsOpen)
                {
                    port.Close();
                    Thread.Sleep(10);
                }

                port ??= new SerialPort(ComName, baudRate, Parity.None, 8, StopBits.One);
                port.Open();
                IsConnect = true;
                SendStatus = EnumSendStatus.Success;
            }
            catch (Exception ex)
            {
                LoggerManager.WriteLog($"{ComName}打开串口失败:{ex.Message}");
                IsConnect = false;
                SendStatus = EnumSendStatus.Error;
            }
        }

        private bool VerifyConn()
        {
            if (port == null)
            {
                InitPort();
            }

            if (port == null || !port.IsOpen)
            {
                TryOpen();
            }

            return port != null && port.IsOpen;
        }

        /// <summary>
        /// 查询网络状态
        /// </summary>
        public override void GetCREG()
        {
            if (!VerifyConn())
            {
                return;
            }
            port?.WriteLine($"AT+CREG?\r");
        }

        /// <summary>
        /// 查询运营商
        /// </summary>
        public override void GetCOPS()
        {
            if (!VerifyConn())
            {
                return;
            }
            port?.WriteLine($"AT+COPS?\r");
        }

        public void SendWriteLine(string str)
        {
            port?.WriteLine($"{str}");
        }
        public void SendWrite(string str)
        {
            port?.Write(str);
        }
        public void SendWrite(byte[] buffer, int offset, int count)
        {
            port?.Write(buffer, offset, count);
        }

        /// <summary>
        /// 清空缓存区
        /// </summary>
        public override void Discard()
        {
            if (!VerifyConn())
            {
                return;
            }
            port?.DiscardInBuffer();
            port?.DiscardOutBuffer();
        }

        public override void Close()
        {
            SendStatus = EnumSendStatus.Error;
            port?.Close();
        }

        public override void GetCSCA()
        {
            if (!VerifyConn())
            {
                return;
            }
            port?.WriteLine($"AT+CSCA?\r");
        }

        public async override Task<string> SetCSCA(string phone)
        {
            if (!VerifyConn())
            {
                return "ERROR";
            }
            SetCenterPhoneTcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);

            port?.WriteLine($"AT+CSCA=\"{phone}\"\r");
            this.CenterPhone = phone;

            Task<string> setCenterTask = SetCenterPhoneTcs.Task;
            var completed = await Task.WhenAny(setCenterTask, Task.Delay(3000));

            if (completed != setCenterTask)
            {
                SetCenterPhoneTcs = null;
                LoggerManager.WriteLog($"COM:{ComName} 设置中心号:{phone} 超时");
                return "ERROR";
            }
            var result = await setCenterTask;
            SetCenterPhoneTcs = null;
            return result;
        }
    }
}
