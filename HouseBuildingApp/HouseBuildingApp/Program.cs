using System;
using System.IO;

namespace HouseBuildingApp
{
    public class Logger
    {
        private string logFilePath = "log.txt";
        private string logFormat = "{DateTime} [{MessageType}] {UserName}: {Message}";

        public Logger(string logFile = null, string format = null)
        {
            if (!string.IsNullOrEmpty(logFile)) logFilePath = logFile;
            if (!string.IsNullOrEmpty(format)) logFormat = format;
        }

        public void Log(string messageType, string message)
        {
            string logMessage = logFormat
                .Replace("{DateTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .Replace("{MessageType}", messageType)
                .Replace("{UserName}", Environment.UserName)
                .Replace("{Message}", message);

            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
    }

    public class HouseBuilder
    {
        private Logger _logger;

        public HouseBuilder(Logger logger)
        {
            _logger = logger;
        }

        public void BuildFoundation()
        {
            _logger.Log("INFO", "Начато строительство фундамента.");
            Console.WriteLine("Фундамент построен.");
            _logger.Log("INFO", "Фундамент успешно построен.");
        }

        public void BuildWalls()
        {
            _logger.Log("INFO", "Начато строительство стен.");
            Console.WriteLine("Стены построены.");
            _logger.Log("INFO", "Стены успешно построены.");
        }

        public void BuildRoof()
        {
            _logger.Log("INFO", "Начато строительство крыши.");
            Console.WriteLine("Крыша построена.");
            _logger.Log("INFO", "Крыша успешно построена.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger();

            HouseBuilder builder = new HouseBuilder(logger);

            try
            {
                builder.BuildFoundation();
                builder.BuildWalls();
                builder.BuildRoof();
                logger.Log("INFO", "Дом успешно построен.");
            }
            catch (Exception ex)
            {
                logger.Log("ERROR", $"Произошла ошибка: {ex.Message}");
            }

            Console.WriteLine("Программа завершена.");
        }
    }
}
