using SingletonPattern_Log_CRUD.BaseClasses;

namespace SingletonPattern_Log_CRUD.Services
{
    public class LoggerService : Singleton<LoggerService>
    {
        private LoggerService()
        {
        }

        public void Log(string ip, string buttonName, string action)
        {
            string logMessage = $"{DateTime.Now} - IP: {ip}, Button: {buttonName}, Acction: {action}";

            string logFilePath = "log.txt";

            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine(logMessage);
                writer.WriteLine(Environment.NewLine);
            }
        }
    }

}
