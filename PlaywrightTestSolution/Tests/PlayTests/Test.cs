using PlaywrightTestSolution.BusinessLogic.Actions;
using PlaywrightTestSolution.BusinessLogic.Helpers;
using PlaywrightTestSolution.BusinessLogic.PageObjects.Pages;

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
            _baseActions = new BaseActions(driver);
            _loginPage = new LoginPage(driver);
        }

        [Test]
        public async Task LoginAsViewer()
        {
            const string EXPECTED_PAGE_URL = "https://weather-drone-monitoring.web.app/dashboard";

            await _loginPage.NavigateTo();
            await _loginPage.LoginByEmail("TestUser1");

            await Waiters.WaitForCondition(() => _baseActions.GetCurrentURL().Equals(EXPECTED_PAGE_URL));
        }
    }
}
