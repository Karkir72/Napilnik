using System;
using System.IO;

namespace Napilnik.Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            Pathfinder filePathfinder = new Pathfinder(new FileLogWriter());
            Pathfinder consolePathfinder = new Pathfinder(new ConsoleLogWriter());
            Pathfinder consoleFridayPathfinder = new Pathfinder(new FridayConsoleLogWriter(new ConsoleLogWriter()));
            Pathfinder fileFridayPathfinder = new Pathfinder(new FridayConsoleLogWriter(new FileLogWriter()));
            Pathfinder chainPathfinder = new Pathfinder(new LoggerWriterChain(new ILogger[2] { new ConsoleLogWriter(), new FridayConsoleLogWriter(new FileLogWriter()) }));
        }
    }

    interface ILogger
    {
        void WriteError(string message);
    }

    class Pathfinder
    {
        private ILogger _logger;

        public Pathfinder(ILogger logger)
        {
            _logger = logger;
        }

        public void Find(string message)
        {
            _logger.WriteError(message);
        }
    }

    class FileLogWriter : ILogger
    {
        public void WriteError(string message)
        {
            File.WriteAllText("log.txt", message);
        }
    }

    class ConsoleLogWriter : ILogger
    {
        public void WriteError(string message)
        {
            Console.WriteLine(message);
        }
    }

    class FridayConsoleLogWriter : ILogger
    {
        private ILogger _logger;

        public FridayConsoleLogWriter(ILogger logger)
        {
            _logger = logger;
        }

        public void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                Console.WriteLine(message);
        }
    }

    class LoggerWriterChain : ILogger
    {
        private ILogger[] _loggers;

        public LoggerWriterChain(ILogger[] loggers)
        {
            _loggers = loggers;
        }

        public void WriteError(string message)
        {
            foreach (ILogger logger in _loggers)
                logger.WriteError(message);
        }
    }
}
