using LD.SetContentPhone.Managers;
using LD.SetContentPhone.Models;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LD.SetContentPhone
{
    public partial class SetSystem : UIPage
    {
        public SetSystem()
        {
            InitializeComponent();

            InitConfig();
        }

        private void InitConfig()
        {
            var data = JsonHelper.Read<ConfigModel>() ?? new ConfigModel();

            txt_SendContent.Text = data.SendContent;
            txt_SendPhone.Text = data.SendPhone;

            SmsConfig.ConfigModel = data;
            SmsConfig.SendPhones = SplitLines(data.SendPhone);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string sendContent = txt_SendContent.Text.Trim();
            string sendPhone = txt_SendPhone.Text.Trim();

            if (string.IsNullOrEmpty(sendContent))
            {
                this.ShowWarningDialog2("发送内容为空");
                return;
            }

            if (string.IsNullOrEmpty(sendPhone))
            {
                this.ShowWarningDialog2("发送号码为空");
                return;
            }

            ConfigModel data = new ConfigModel();
            data.SendContent = sendContent;
            data.SendPhone = sendPhone;

            SmsConfig.ConfigModel = data;
            SmsConfig.SendPhones = SplitLines(data.SendPhone);
            JsonHelper.Write(data);
            this.ShowSuccessDialog2("保存成功");
        }

        private List<string> SplitLines(string text)
        {
            return (text ?? string.Empty)
                .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(item => item.Trim())
                .Where(item => !string.IsNullOrEmpty(item))
                .ToList();
        }
    }
}
