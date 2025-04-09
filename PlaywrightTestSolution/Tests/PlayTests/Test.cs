using PlaywrightTestSolution.BusinessLogic.PageObjects.Pages;
using PlaywrightTestSolution.BusinessLogic.Actions;
using PlaywrightTestSolution.BusinessLogic.Helpers;
using NUnit.Framework.Internal;

namespace PlaywrightTestSolution.Tests.PlayTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class Test : BaseTest
    {
        private BaseActions _baseActions;
        private LoginPage _loginPage;

        [SetUp]
        public void Setup()
        {
            _baseActions = new BaseActions(driver, logger!);
            _loginPage = new LoginPage(driver, logger!);
        }

        // Login in application
        // Verify that user is logged in
        [Test]
        public async Task LoginAsViewer()
        {
            const string EXPECTED_PAGE_URL = "https://weather-drone-monitoring.web.app/dashboard";

            await _loginPage.NavigateTo();
            await _loginPage.LoginByEmail("TestUser1");
            logger!.Information("User logged in.");

            await Waiters.WaitForCondition(() => _baseActions.GetCurrentURL().Equals(EXPECTED_PAGE_URL));
        }

        // Login in application
        // Verify that user is logged in
        // Fail test
        [Test]
        public async Task FailingTest()
        {
            const string EXPECTED_PAGE_URL = "https://weather-drone-monitoring.web.app/dashboard";

            await _loginPage.NavigateTo();
            await _loginPage.LoginByEmail("TestUser1");
            logger!.Information("User logged in.");

            await Waiters.WaitForCondition(() => _baseActions.GetCurrentURL().Equals(EXPECTED_PAGE_URL));
            Assert.Fail("This test is expected to fail.");
        }

        // Login in application
        // Verify that user is logged in
        // Fail test
        [Test]
        public async Task AnotherFailingTest()
        {
            const string EXPECTED_PAGE_URL = "https://weather-drone-monitoring.web.app/dashboard";

            await _loginPage.NavigateTo();
            await _loginPage.LoginByEmail("TestUser1");
            logger!.Information("User logged in.");

            await Waiters.WaitForCondition(() => _baseActions.GetCurrentURL().Equals(EXPECTED_PAGE_URL));
            CustomAssertions.BeTrue(false, "This test is expected to fail.");
        }
    }
}
