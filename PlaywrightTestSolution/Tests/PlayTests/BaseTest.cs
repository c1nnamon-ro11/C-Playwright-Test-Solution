using Microsoft.Playwright.NUnit;
using PlaywrightTestSolution.BusinessLogic.Drivers;

namespace PlaywrightTestSolution.Tests.PlayTests
{
    public class BaseTest : PageTest
    {
        public Driver driver { get; set; }

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            driver = new Driver();
            await Task.CompletedTask;
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
           await driver.Dispose();
        }
    }
}
