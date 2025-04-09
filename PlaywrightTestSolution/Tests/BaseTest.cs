using PlaywrightTestSolution.BusinessLogic.Drivers;
using PlaywrightTestSolution.BusinessLogic.Helpers;
using Microsoft.Playwright.NUnit;
using NUnit.Framework.Interfaces;
using Serilog.Events;
using Serilog;

namespace PlaywrightTestSolution.Tests
{
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)] // Has to be for Parallel test execution
    public class BaseTest : PageTest
    {
        public Driver driver { get; private set; }
        public ILogger? logger { get; private set; }
        private ScreenshotTaker? screenshotTaker;

        private const bool ADD_SCREENSHOT_ON_FAIL = true; // Set to false if you don't want to take a screenshot on test failure
        private const LogEventLevel LOGS_LEVEL = LogEventLevel.Information; // Set appropriate lvl to save logs:
                                                                            // 1) Verbose - BaseActions/Waiters
                                                                            // 2) Debug - PO methods
                                                                            // 3) Information - Test methods, general information
                                                                            // 4) All higher levels - no logs
        private readonly string CURRENT_DATE = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

        public const int Sec = 1000; // 1 sec
        public const int Min = 60*Sec; // 1 Min
        public const int TwoMin = 2 * Min; // 2 Min
        public const int FiveMin = 5 * Min; // 5 Min

        [SetUp]
        public async Task SetUp()
        {
            driver = new Driver();

            if (ADD_SCREENSHOT_ON_FAIL)
            {
                screenshotTaker = new ScreenshotTaker(driver.Page);
            }
            logger = new Logger().GetInstance(CURRENT_DATE, TestContext.CurrentContext.Test.Name, LOGS_LEVEL);
            CustomAssertions.InitializeLogger(logger!);
            logger.Information($"{TestContext.CurrentContext.Test.Name} started.");
            await Task.CompletedTask;
        }

        [TearDown]
        public async Task TearDown()
        {
            bool isTestPassed = TestContext.CurrentContext.Result.Outcome == ResultState.Success;

            if (ADD_SCREENSHOT_ON_FAIL && !isTestPassed)
            {
                await screenshotTaker!.TakeScreenshot(TestContext.CurrentContext.Test.Name, CURRENT_DATE);
            }
            if (isTestPassed)
            {
                logger!.Information($"{TestContext.CurrentContext.Test.Name} successfully passes.");
            }
            else
            {
                logger!.Information($"{TestContext.CurrentContext.Test.Name} failed.");
            }           

            await driver.DisposeAsync();
        }
    }
}
