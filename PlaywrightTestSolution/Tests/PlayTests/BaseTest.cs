using Microsoft.Playwright.NUnit;
using PlaywrightTestSolution.BusinessLogic.Drivers;

namespace PlaywrightTestSolution.Tests.PlayTests
{
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)] // Has to be for Parallel test execution
    public class BaseTest : PageTest
    {
        public Driver driver { get; set; }

        [SetUp]
        public async Task SetUp()
        {
            driver = new Driver();
            await Task.CompletedTask;
        }

        [TearDown]
        public async Task TearDown()
        {
           await driver.DisposeAsync();
        }
    }
}
