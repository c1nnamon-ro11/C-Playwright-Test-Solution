using Microsoft.Playwright;

namespace PlaywrightTestSolution.BusinessLogic.Drivers
{
    public class Driver
    {
        private readonly Task<IPage> _page;
        private IBrowser? _browser;
        private const bool IS_CHROME_TARGET_BROWSER = false;

        public Driver()
        {
            _page = InitializePlaywright();
        }

        public IPage Page => GetInstanse().Result;

        public async Task<IPage> GetInstanse()
        {
            if (_page == null)
            {
                await InitializePlaywright();
            }
            return await _page!;           
        }


        private async Task<IPage> InitializePlaywright()
        {
            // Playwright
            var playwright = await Playwright.CreateAsync();

            // Browser
            var browserTypeLaunchOptions = new BrowserTypeLaunchOptions()
            {
                Headless = false
            };
            _browser = IS_CHROME_TARGET_BROWSER ? 
                await playwright.Chromium.LaunchAsync(browserTypeLaunchOptions) :
                await playwright.Firefox.LaunchAsync(browserTypeLaunchOptions);
            var context = await _browser.NewContextAsync();
            return await context.NewPageAsync();
        }

        public async Task Dispose()
        {
            await _page.Result.CloseAsync();
            await _browser!.CloseAsync();
        }
    }
}
