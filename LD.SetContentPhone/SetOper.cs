using LD.SetContentPhone.Managers;
using LD.SetContentPhone.Models;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LD.SetContentPhone
{
    public partial class SetOper : UIPage
    {
        public SetOper()
        {
            InitializeComponent();
        }

        private bool _isMock = false;
        public RunManager RunManager { get; set; } = new RunManager();
        private System.Windows.Forms.Timer? uiTimer;
        Dictionary<string, AbstractSendManager> dic = new Dictionary<string, AbstractSendManager>();

        private void InitDic()
        {
            if (dic.Any())
            {
                return;
            }

            if (_isMock)
            {
                InitMockCom();
                return;
            }
            InitCom();
        }

        private RunState _currentState = RunState.None;
        private void UpdateButtonState()
        {
            switch (_currentState)
            {
                case RunState.None:
                    btn_Start.Enabled = true;
                    btn_Pause.Enabled = false;
                    btn_Continue.Enabled = false;
                    btn_Stop.Enabled = false;
                    break;

                case RunState.Running:
                    btn_Start.Enabled = false;
                    btn_Pause.Enabled = true;
                    btn_Continue.Enabled = false;
                    btn_Stop.Enabled = true;
                    break;

                case RunState.Paused:
                    btn_Start.Enabled = false;
                    btn_Pause.Enabled = false;
                    btn_Continue.Enabled = true;
                    btn_Stop.Enabled = true;
                    break;

                case RunState.Stopped:
                    btn_Start.Enabled = true;
                    btn_Pause.Enabled = false;
                    btn_Continue.Enabled = false;
                    btn_Stop.Enabled = false;
                    break;
            }
        }

        private void InitCom()
        {
            // 获取所有串口号
            string[] ports = SerialPort.GetPortNames();

            int baudRate = 115200;
            // 遍历所有串口 并且创建连接
            foreach (string port in ports)
            {
                var item = new SerialPortManager(port, baudRate);
                item.Discard();
                item.GetCREG();
                item.GetCOPS();
                Thread.Sleep(100);

                item.SetMode();
                dic.Add(port, item);
            }

            //不自动生成列
            uiDataGridView1.AutoGenerateColumns = false;
            DataGridRefresh();
            // 统一设置编码 todo? 是否设置一次 可以循环发送还是每次发送设置
        }

        private void InitMockCom()
        {
            for (int i = 1; i<=16; i++)
            {
                string prot = $"com{i}";
                var item = new LogSendManager(prot);
                item.Discard();
                item.GetCREG();
                item.GetCOPS();
                item.SetMode();
                Thread.Sleep(100);

                dic.Add(prot, item);
            }

            //不自动生成列
            uiDataGridView1.AutoGenerateColumns = false;
            DataGridRefresh();
        }

        private void DataGridRefresh()
        {
            this.Invoke((MethodInvoker)(() =>
            {
                uiDataGridView1.DataSource = null;
                uiDataGridView1.DataSource = new List<AbstractSendManager>(dic.Values);

                if (RunManager != null)
                {
                    led_SendCount.Value = RunManager.ConsumedCount;
                }
            }));
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SmsConfig.ConfigModel.SendContent))
            {
                this.ShowErrorDialog2("发送内容为空");
                return;
            }

            if (SmsConfig.SendPhones == null || SmsConfig.SendPhones.Count == 0)
            {
                this.ShowErrorDialog2("发送手机号为空");
                return;
            }

            RunManager.Start();

            if (RunManager.IsRunning)
            {
                _currentState = RunState.Running;
                UpdateButtonState();
            }
        }

        private void btn_Pause_Click(object sender, EventArgs e)
        {
            _currentState = RunState.Paused;
            UpdateButtonState();

            RunManager.Pause();
        }

        private void btn_Continue_Click(object sender, EventArgs e)
        {
            _currentState = RunState.Running;
            UpdateButtonState();

            RunManager.Resume();
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            _currentState = RunState.Stopped;
            UpdateButtonState();

            RunManager.Stop();
        }

        private void UiTimer_Tick(object? sender, EventArgs e)
        {
            DataGridRefresh();
        }

        bool isFormLoaded = false;

        private async void SetOper_Initialize(object sender, EventArgs e)
        {
            if (isFormLoaded)
            {
                return;
            }

            isFormLoaded = true;
            using (var loading = new LoadingForm())
            {
                this.Enabled = false;
                loading.Show();

                await Task.Run(() =>
                {
                    InitDic();
                });

                loading.Close();

                RunManager = new RunManager();
                RunManager.SetDicCom(dic);
                RunManager.ShowMsg += ShowMsg;
                RunManager.Completed += RunCompleted;

                uiTimer = new System.Windows.Forms.Timer();
                uiTimer.Interval = 1000; // 1秒
                uiTimer.Tick += UiTimer_Tick;
                uiTimer.Start();

                UpdateButtonState();
                this.Enabled = true;
            }
        }
        private void ShowMsg(string msg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => ShowMsg(msg)));
                return;
            }

            this.ShowWarningDialog2(msg);
        }

        private void RunCompleted()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(RunCompleted));
                return;
            }

            _currentState = RunState.Stopped;
            UpdateButtonState();
            DataGridRefresh();
            this.ShowSuccessDialog2("发送完成");
        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            // 打开文件选择框
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "请选择文本文件";
            openFileDialog.Filter = "文本文件 (*.txt)|*.txt|所有文件 (*.*)|*.*";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;

                List<string> lines = File.ReadAllLines(path)
                    .Select(line => line.Trim())        // 去除首尾空格
                    .Where(line => !string.IsNullOrEmpty(line)) // 过滤空行
                    .ToList();

                RunManager.ResetDataQueue(lines);
                LoggerManager.WriteLog($"导入中心号码数量:{lines.Count} 文件:{path}");

                this.ShowSuccessDialog2($"当前导入数量:{lines.Count}");
            }
            else
            {
                this.ShowWarningDialog2("取消文件选择");
            }
        }
    }
}
