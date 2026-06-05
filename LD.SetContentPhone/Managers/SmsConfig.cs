using LD.SetContentPhone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD.SetContentPhone.Managers
{
    public class SmsConfig
    {
        public static string DataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SetContentPhone\\";
        public static string JsonPath = DataPath + "appsetting.json";
        public static ConfigModel ConfigModel { get; set; } = new ConfigModel();
        public static List<string> SendPhones { get; set; } = new List<string>();
    }

}
