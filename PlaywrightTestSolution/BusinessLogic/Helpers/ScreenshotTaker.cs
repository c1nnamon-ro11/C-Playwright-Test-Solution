using Microsoft.Playwright;

namespace PlaywrightTestSolution.BusinessLogic.Helpers
{
    public class ScreenshotTaker
    {
        private IPage _page;
        private const string RELATIVE_PATH = @"\Tests\TestsOutput\FailuresScreenshots";
        private const bool FULL_PAGE_SCREENSHOT = false;

        public ScreenshotTaker(IPage page)
        {
            _page = page;
        }

        public async Task TakeScreenshot(string testName)
        {
            string currentDate = (DateTime.Now).ToString("yyyy-MM-dd_HH-mm-ss");
            string workDirectory = Directory.GetCurrentDirectory() + RELATIVE_PATH;
            string screenshotPath = Path.Combine(workDirectory, $"{TestContext.CurrentContext.Test.Name}");
            string screenshotName = $"{TestContext.CurrentContext.Test.Name}_{currentDate}.png";

            try
            {
                // Create new folder if it not exists
                if (!Directory.Exists(screenshotPath))
                {
                    Directory.CreateDirectory(screenshotPath);
                }
                await _page.ScreenshotAsync(new PageScreenshotOptions
                {
                    Path = Path.Combine(screenshotPath, screenshotName),
                    FullPage = FULL_PAGE_SCREENSHOT
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error taking screenshot: {ex.Message}");
            }
        }
    }
}
