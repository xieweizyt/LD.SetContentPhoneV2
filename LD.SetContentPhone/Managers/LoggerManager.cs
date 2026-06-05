using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System.Reflection;

namespace LD.SetContentPhone.Managers
{
    public static class LoggerManager
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(LoggerManager));

        public static void Init()
        {
            string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }

            Assembly repositoryAssembly = Assembly.GetEntryAssembly() ?? typeof(LoggerManager).Assembly;
            var repository = (Hierarchy)LogManager.GetRepository(repositoryAssembly);
            if (repository.Configured)
            {
                return;
            }

            var layout = new PatternLayout("%date{yyyy-MM-dd HH:mm:ss.fff} %-5level %message%newline");
            layout.ActivateOptions();

            var appender = new RollingFileAppender
            {
                Name = "FileAppender",
                File = Path.Combine(logDir, "run-"),
                AppendToFile = true,
                RollingStyle = RollingFileAppender.RollingMode.Date,
                DatePattern = "yyyy-MM-dd'.log'",
                StaticLogFileName = false,
                Layout = layout,
                LockingModel = new FileAppender.MinimalLock(),
                Encoding = System.Text.Encoding.UTF8
            };
            appender.ActivateOptions();

            BasicConfigurator.Configure(repository, appender);
            repository.Root.Level = Level.Info;
            repository.Configured = true;
        }

        public static void WriteLog(string message)
        {
            Log.Info(message);
        }

        public static void WriteError(string message, Exception ex)
        {
            Log.Error(message, ex);
        }
    }
}
