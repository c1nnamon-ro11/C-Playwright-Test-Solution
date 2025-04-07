using Microsoft.Playwright.NUnit;
using NUnit.Framework.Interfaces;
using PlaywrightTestSolution.BusinessLogic.Drivers;
using PlaywrightTestSolution.BusinessLogic.Helpers;

namespace PlaywrightTestSolution.Tests.PlayTests
{
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)] // Has to be for Parallel test execution
    public class BaseTest : PageTest
    {
        public Driver driver { get; set; }
        private ScreenshotTaker screenshotTaker;

        [SetUp]
        public async Task SetUp()
        {
            driver = new Driver();
            screenshotTaker = new ScreenshotTaker(driver.Page);
            await Task.CompletedTask;
        }

        [TearDown]
        public async Task TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                await screenshotTaker.TakeScreenshot(TestContext.CurrentContext.Test.Name);
            }
            await driver.DisposeAsync();
        }
    }
}
