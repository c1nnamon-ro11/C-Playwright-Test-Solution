using Microsoft.Playwright;

namespace PlaywrightTestSolution.Connectors.Locators
{
    public class LoginPageLocators
    {
        private IPage _page;
        public LoginPageLocators(IPage page)
        {
            _page = page;
        }

        public ILocator AuthorizationBox => _page.Locator("//div[@class='authorization-box']"); 
        public ILocator UserNameField => _page.Locator("#email");
        public ILocator PasswordField => _page.Locator("#password");
        public ILocator LoginButton => _page.Locator("//input[@value='Log In']");
    }
}
