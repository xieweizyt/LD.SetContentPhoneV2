using LD.SetContentPhone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LD.SetContentPhone.Managers
{
    public class LogSendManager : AbstractSendManager
    {


        public LogSendManager(string comName)
        {
            this.ComName = comName;
            this.SendStatus = Models.EnumSendStatus.Success;
            this.Carrier = "测试";

        }

        public override void Close()
        {
            LoggerManager.WriteLog($"{ComName}调用Close");
        }

        public override void Discard()
        {
            LoggerManager.WriteLog($"{ComName}调用Discard");
        }

        public override void GetCOPS()
        {
            LoggerManager.WriteLog($"{ComName}调用GetCOPS");
        }

        public override void GetCREG()
        {
            LoggerManager.WriteLog($"{ComName}调用GetCREG");
        }

        public override void GetCSCA()
        {
            LoggerManager.WriteLog($"{ComName}调用获取中心号码");
        }

        public override void SendSmsByPhone(string phone, string message)
        {
            LoggerManager.WriteLog($"{ComName}向{phone}发送:{message}");

            this.CurrentSendPhone = phone;
            this.SendCount++;

            this.SendSuccessCount++;
            this.RunStatus = Models.EnumRunStatus.None;
        }

        public async override Task<string> SendSmsByPhoneAsync(string phone, string message, CancellationToken cancellationToken = default)
        {
            SendSmsByPhone(phone, message);
            await Task.Delay(200, cancellationToken);
            return "OK";
        }

        public async override Task<string> SetCSCA(string phone)
        {
            LoggerManager.WriteLog($"{ComName}调用设置中心号码={phone}");
            this.CenterPhone = phone;
            SetCenterPhoneTcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);

            _ = Task.Run(() =>
            {
                Thread.Sleep(1000);
                SetCenterPhoneTcs?.TrySetResult("OK");
            });

            var completed = await Task.WhenAny(
            SetCenterPhoneTcs.Task,
            Task.Delay(3000));

            if (completed != SetCenterPhoneTcs.Task)
            {
                SetCenterPhoneTcs = null;
                return "ERROR";
            }
            var result = await SetCenterPhoneTcs.Task;
            SetCenterPhoneTcs = null;
            return result;
        }

        public override void SetMode()
        {
            LoggerManager.WriteLog($"{ComName}调用SetMode");
        }

        public override void TryOpen()
        {
            this.SendStatus = Models.EnumSendStatus.Success;
            LoggerManager.WriteLog($"{ComName}调用TryOpen");
        }
    }
}
