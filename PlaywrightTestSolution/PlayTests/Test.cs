using PlaywrightTestSolution.Actions;
using PlaywrightTestSolution.PlayTests;
using PlaywrightTestSolution.PageObjects;
using FluentAssertions;

namespace PlaywrightTestSolution.Tests
{
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
        public async Task Test1()
        {
            const string EXPECTED_PAGE_URL = "https://weather-drone-monitoring.web.app/dashboard";

            await _loginPage.NavigateTo();
            await _loginPage.LoginByEmail("TestUser1");

            await Task.Delay(5000);
            _baseActions.GetCurrentURL().Should().BeEquivalentTo(EXPECTED_PAGE_URL);
        }
    }
}
