using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace LD.SetContentPhone.Managers
{
    public static class JsonHelper
    {
        private static readonly object _lock = new object();
        private static string filePath = SmsConfig.JsonPath;

        /// <summary>
        /// 写入 JSON 文件（自动创建文件和目录）
        /// </summary>
        public static void Write<T>(T data, bool indented = true)
        {
            lock (_lock)
            {
                EnsureDirectory(filePath);

                var options = new JsonSerializerOptions
                {
                    WriteIndented = indented
                };

                string json = JsonSerializer.Serialize(data, options);
                File.WriteAllText(filePath, json);
            }
        }

        /// <summary>
        /// 读取 JSON 文件，如不存在返回默认值
        /// </summary>
        public static T Read<T>()
        {
            lock (_lock)
            {
                if (!File.Exists(filePath))
                    return default!;

                string json = File.ReadAllText(filePath);

                return JsonSerializer.Deserialize<T>(json)!;
            }
        }

        /// <summary>
        /// 读取为JsonNode（适合动态修改JSON结构）
        /// </summary>
        public static JsonNode ReadNode()
        {
            lock (_lock)
            {
                if (!File.Exists(filePath))
                    return new JsonObject();

                string json = File.ReadAllText(filePath);
                return JsonNode.Parse(json)!;
            }
        }

        /// <summary>
        /// 写入 JsonNode（动态 JSON 修改后保存）
        /// </summary>
        public static void WriteNode(JsonNode node, bool indented = true)
        {
            lock (_lock)
            {
                EnsureDirectory(filePath);

                var options = new JsonSerializerOptions
                {
                    WriteIndented = indented
                };

                string json = node.ToJsonString(options);
                File.WriteAllText(filePath, json);
            }
        }

        /// <summary>
        /// 确保文件目录存在
        /// </summary>
        private static void EnsureDirectory(string filePath)
        {
            string? dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (!File.Exists(filePath))
                File.WriteAllText(filePath, "{}"); // 创建空 JSON
        }
    }
}
