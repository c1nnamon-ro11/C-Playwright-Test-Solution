using PlaywrightTestSolution.Actions;
using PlaywrightTestSolution.Drivers;
using PlaywrightTestSolution.PageObjects.Locators;
using PlaywrightTestSolution.Users;

namespace PlaywrightTestSolution.PageObjects
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
            await _driver.Page.GotoAsync(_loginPageURL);
        }

        public async Task LoginByEmail(string userName)
        {
            List<UserModel> users = UserDeselializer.GetUsers();
            var targetUser = users.Select(user => user).Where(user => user.UserName == userName).First();

            await _baseActions.SetInput(_loginPageLocators.PasswordField, targetUser.Password!);
            await _baseActions.SetInput(_loginPageLocators.PasswordField, targetUser.Password!);

            await _loginPageLocators.LoginButton.ClickAsync();
        }
    }
}
