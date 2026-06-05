using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD.SetContentPhone.Models
{
    public class ConfigModel
    {
        /// <summary>
        /// 发送内容
        /// </summary>
        public string SendContent { get; set; } = "测试发送内容";
        /// <summary>
        /// 验证手机
        /// </summary>
        public string SendPhone { get; set; } = "1234567890";
    }
}
