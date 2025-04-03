using PlaywrightTestSolution.Drivers;

namespace PlaywrightTestSolution.PlayTests
{
    public class BaseTest
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
           await driver.Dispose();
        }
    }
}
