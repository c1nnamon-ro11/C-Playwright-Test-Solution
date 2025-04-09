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

        public async Task LoginByUserName(string userName)
        {
            UserModel targetUser;

            try
            {
                targetUser = UserDeselializer.GetUserByParameter("UserName", userName);
            }
            catch
            {
                _logger.Information($"User with email {userName} not found in the Users.json file.");
                throw new Exception($"User with email {userName} not found in the Users.json file.");
            }

            _logger.Debug($"Enter user email and password");
            await _baseActions.SetInput(_loginPageLocators.UserNameField, targetUser.UserEmail!);
            await _baseActions.SetInput(_loginPageLocators.PasswordField, targetUser.Password!);

            _logger.Debug($"Click login");
            await _loginPageLocators.LoginButton.ClickAsync();
        }

        public async Task LoginByRole(string userRole)
        {
            UserModel targetUser;

            try
            {
                targetUser = UserDeselializer.GetUserByParameter("Role", userRole);
            }
            catch
            {
                _logger.Information($"User with Role {userRole} not found in the Users.json file.");
                throw new Exception($"User with Role {userRole} not found in the Users.json file.");
            }

            _logger.Debug($"Enter user email and password");
            await _baseActions.SetInput(_loginPageLocators.UserNameField, targetUser.UserEmail!);
            await _baseActions.SetInput(_loginPageLocators.PasswordField, targetUser.Password!);
            await Task.Delay(500);

            _logger.Debug($"Click login");
            await _loginPageLocators.LoginButton.ClickAsync();
        }
    }
}
