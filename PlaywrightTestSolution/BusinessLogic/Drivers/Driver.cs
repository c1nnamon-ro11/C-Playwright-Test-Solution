using Microsoft.Playwright;

namespace PlaywrightTestSolution.BusinessLogic.Drivers
{
    public class Driver : IAsyncDisposable
    {
        private Task<IPage>? _page;
        private IBrowser? _browser;
        private IBrowserContext? _context;
        private const bool IS_CHROME_TARGET_BROWSER = true;
        private const bool IS_HEADLESS = false;

        public Driver()
        {
            _page = InitializePlaywright();
        }

        public IPage Page => GetInstance().Result;
        public IPlaywright Playwright { get; private set; } = null!;

        public async Task<IPage> GetInstance()
        {
            if (_page == null)
            {
                _page = InitializePlaywright();
            }
            return await _page!;
        }

        private async Task<IPage> InitializePlaywright()
        {
            // Playwright
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            // Browser
            var browserTypeLaunchOptions = new BrowserTypeLaunchOptions()
            {
                Headless = IS_HEADLESS
            };
            _browser = IS_CHROME_TARGET_BROWSER ?
                await Playwright.Chromium.LaunchAsync(browserTypeLaunchOptions) :
                await Playwright.Firefox.LaunchAsync(browserTypeLaunchOptions);
            // Context
            var browserNewContextOptions = new BrowserNewContextOptions()
            {
                ColorScheme = ColorScheme.Light,
                ViewportSize = new()
                {
                    Width = 2560,
                    Height = 1449
                },
                BaseURL = "https://playwright.dev/dotnet/",           
            };
            _context = await _browser.NewContextAsync();
            return await _context.NewPageAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (_page != null)
            {
                await (await _page).CloseAsync();
            }
            if (_context != null)
            {
                await _context.CloseAsync();
            }
            if (_browser != null)
            {
                await _browser.CloseAsync();
            }
            Playwright?.Dispose();
        }       
    }
}