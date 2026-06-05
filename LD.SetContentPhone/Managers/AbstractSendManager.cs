using LD.SetContentPhone.Models;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD.SetContentPhone.Managers
{
    public abstract class AbstractSendManager
    {
        public readonly object LockObj = new object();
        public EnumRunStatus RunStatus { get; set; } = EnumRunStatus.None;
        /// <summary>
        /// com口
        /// </summary>
        public string ComName { get; set; } = string.Empty;
        /// <summary>
        /// 运营商
        /// </summary>
        public string Carrier { get; set; } = string.Empty;
        /// <summary>
        /// 号码
        /// </summary>
        public string CenterPhone { get; set; } = string.Empty;
        /// <summary>
        /// 已发送成功次数
        /// </summary>
        public int SendSuccessCount { get; set; }
        /// <summary>
        /// 已发送失败次数
        /// </summary>
        public int SendFailCount { get; set; }
        /// <summary>
        /// 已发送次数
        /// </summary>
        public int SendCount { get; set; }
        /// <summary>
        /// 当前发送号码
        /// </summary>
        public string CurrentSendPhone { get; set; } = string.Empty;
        /// <summary>
        /// 发送状态
        /// </summary>
        public EnumSendStatus SendStatus { get; set; } = EnumSendStatus.Error;
        public TaskCompletionSource<string>? SetCenterPhoneTcs;
        public TaskCompletionSource<string>? SendSmsTcs;

        /// <summary>
        /// 尝试开启
        /// </summary>
        public abstract void TryOpen();
        /// <summary>
        /// 设置模式
        /// </summary>
        public abstract void SetMode();
        /// <summary>
        /// 获取网络
        /// </summary>
        public abstract void GetCREG();
        /// <summary>
        /// 获取运营商
        /// </summary>
        public abstract void GetCOPS();
        /// <summary>
        /// 获取中心号码
        /// </summary>
        public abstract void GetCSCA();
        /// <summary>
        /// 设置中心号码
        /// </summary>
        public abstract Task<string> SetCSCA(string phone);
        public abstract void Discard();
        public abstract void Close();
        public abstract void SendSmsByPhone(string phone, string message);

        public virtual Task<string> SendSmsByPhoneAsync(string phone, string message, CancellationToken cancellationToken = default)
        {
            SendSmsByPhone(phone, message);
            return Task.FromResult("OK");
        }

        public void RecordSendFail(string phone)
        {
            CurrentSendPhone = phone;
            SendCount++;
            SendFailCount++;
            RunStatus = EnumRunStatus.None;
        }
    }
}
