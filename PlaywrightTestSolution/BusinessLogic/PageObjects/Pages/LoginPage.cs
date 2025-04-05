using PlaywrightTestSolution.BusinessLogic.Actions;
using PlaywrightTestSolution.BusinessLogic.Drivers;
using PlaywrightTestSolution.BusinessLogic.Helpers;
using PlaywrightTestSolution.BusinessLogic.Helpers.Users;
using PlaywrightTestSolution.Connectors.Locators;

namespace PlaywrightTestSolution.BusinessLogic.PageObjects.Pages
{
    public class LoginPage
    {
        private readonly Driver _driver;
        private readonly BaseActions _baseActions;
        private readonly LoginPageLocators _loginPageLocators;

        private const string _loginPageURL = "https://weather-drone-monitoring.web.app/sign-in";

        public LoginPage(Driver driver)
        {
            _driver = driver;
            _baseActions = new BaseActions(_driver);
            _loginPageLocators = new LoginPageLocators(_driver.Page);
        }

        public async Task NavigateTo()
        {
            await _baseActions.NavigateTo(_loginPageURL);
        }

        public async Task WaitForPageToLoad()
        {
            await Waiters.WaitForElementToBeVisible(_loginPageLocators.AuthorizationBox);
        }

        public async Task LoginByEmail(string userName)
        {
            List<UserModel> users = UserDeselializer.GetUsers();
            var targetUser = users.Select(user => user).Where(user => user.UserName == userName).First();

            await _baseActions.SetInput(_loginPageLocators.UserNameField, targetUser.UserEmail!);
            await _baseActions.SetInput(_loginPageLocators.PasswordField, targetUser.Password!);

            await _loginPageLocators.LoginButton.ClickAsync();
        }
    }
}
