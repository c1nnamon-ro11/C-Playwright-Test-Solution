﻿using PlaywrightTestSolution.BusinessLogic.PageObjects.Pages;
using PlaywrightTestSolution.BusinessLogic.Actions;
using PlaywrightTestSolution.BusinessLogic.Helpers;
using NUnit.Framework.Internal;
using Allure.NUnit.Attributes;
using Allure.NUnit;

namespace PlaywrightTestSolution.Tests.PlayTests
{
    [AllureNUnit]
    [AllureSuite("Weather Drone Monitoring")]
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
        [Test, Attributes.CustomRetry(2), Timeout(TwoMin)]
        public async Task LoginAsViewer()
        {
            const string EXPECTED_PAGE_URL = "https://weather-drone-monitoring.web.app/dashboard";

            await _loginPage.NavigateTo();
            await _loginPage.LoginByRole("Viewer");
            logger!.Information("User logged in.");

            await Waiters.WaitForCondition(() => _baseActions.GetCurrentURL().Equals(EXPECTED_PAGE_URL));
            await Task.Delay(5000);
        }

        // Login in application
        // Verify that user is logged in
        // Fail test
        [Test, Attributes.CustomRetry(2), Timeout(TwoMin)]
        public async Task FailingTest()
        {
            const string EXPECTED_PAGE_URL = "https://weather-drone-monitoring.web.app/dashboard";

            await _loginPage.NavigateTo();
            await _loginPage.LoginByUserName("TestUser1");
            logger!.Information("User logged in.");

            await Waiters.WaitForCondition(() => _baseActions.GetCurrentURL().Equals(EXPECTED_PAGE_URL));
            Assert.Fail("This test is expected to fail.");
        }

        // Login in application
        // Verify that user is logged in
        // Fail test
        [Test, Retry(2), Timeout(TwoMin)]
        public async Task AnotherFailingTest()
        {
            const string EXPECTED_PAGE_URL = "https://weather-drone-monitoring.web.app/dashboard";

            await _loginPage.NavigateTo();
            await _loginPage.LoginByEmail("vladyslav.shevchuk.mkisk.2021@lpnu.ua");
            logger!.Information("User logged in.");

            await Waiters.WaitForCondition(() => _baseActions.GetCurrentURL().Equals(EXPECTED_PAGE_URL));
            CustomAssertions.BeTrue(false, "This test is expected to fail.");
        }
    }
}
