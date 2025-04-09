using Serilog;
using Serilog.Events;

namespace PlaywrightTestSolution.BusinessLogic.Helpers
{
    public class Logger
    {
        private ILogger? _logger;
        private const string RELATIVE_PATH = @"\Tests\TestsOutput";
        private const string OUTPUT_TEMPLATE = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";

        public ILogger GetInstance(string currentDate, string testName, LogEventLevel minimumLogLevel = LogEventLevel.Information)
        {
            if (_logger == null)
            {
                _logger = InitializeLogger(currentDate, testName, minimumLogLevel);
            }
            return _logger!;
        }

        private ILogger InitializeLogger(string currentDate, string testName, LogEventLevel minimumLogLevel)
        {            
            string workDirectory = Directory.GetCurrentDirectory() + RELATIVE_PATH;
            string logPath = Path.Combine(workDirectory, testName);
            string logName = $"{testName}_{currentDate}.txt";

            return new LoggerConfiguration().MinimumLevel.Is(minimumLogLevel).
                WriteTo.Console(outputTemplate: OUTPUT_TEMPLATE).
                WriteTo.File(Path.Combine(logPath, logName), outputTemplate: OUTPUT_TEMPLATE, 
                    rollOnFileSizeLimit: true, retainedFileCountLimit: 3)
                .CreateLogger();
        }
    }
}
