using PlaywrightTestSolution.BusinessLogic.Actions;
using PlaywrightTestSolution.BusinessLogic.Drivers;
using PlaywrightTestSolution.BusinessLogic.Helpers;
using PlaywrightTestSolution.BusinessLogic.Helpers.Users;
using PlaywrightTestSolution.Connectors.Locators;
using Serilog;

namespace PlaywrightTestSolution.BusinessLogic.PageObjects.Pages
{
    public class LoginPage
    {
        private readonly Driver _driver;
        private readonly BaseActions _baseActions;
        private readonly LoginPageLocators _loginPageLocators;
        private ILogger _logger;

        private const string _loginPageURL = "https://weather-drone-monitoring.web.app/sign-in";

        public LoginPage(Driver driver, ILogger logger)
        {
            _driver = driver;
            _logger = logger;
            _baseActions = new BaseActions(_driver, _logger);
            _loginPageLocators = new LoginPageLocators(_driver.Page);
        }

        public async Task NavigateTo()
        {
            _logger.Debug($"Navigate to application page");
            await _baseActions.NavigateTo(_loginPageURL);
        }

        public async Task WaitForPageToLoad()
        {
            _logger.Debug($"Wait for authorization box to be present.");
            await Waiters.WaitForElementToBeVisible(_loginPageLocators.AuthorizationBox);
        }

        public async Task LoginByEmail(string userName)
        {
            List<UserModel> users = UserDeselializer.GetUsers();
            var targetUser = users.Select(user => user).Where(user => user.UserName == userName).First();

            _logger.Debug($"Enter user email and password");
            await _baseActions.SetInput(_loginPageLocators.UserNameField, targetUser.UserEmail!);
            await _baseActions.SetInput(_loginPageLocators.PasswordField, targetUser.Password!);

            _logger.Debug($"Click login");
            await _loginPageLocators.LoginButton.ClickAsync();
        }
    }
}
