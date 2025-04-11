using Allure.Net.Commons;
using Microsoft.Playwright;

namespace PlaywrightTestSolution.BusinessLogic.Helpers
{
    public class ScreenshotTaker
    {
        private IPage _page;
        private const string RELATIVE_PATH = @"\Tests\TestsOutput";
        private const bool FULL_PAGE_SCREENSHOT = false;

        public ScreenshotTaker(IPage page)
        {
            _page = page;
        }

        public async Task TakeScreenshot(string testName, string currentDate)
        {
            string workDirectory = Directory.GetCurrentDirectory() + RELATIVE_PATH;
            string screenshotPath = Path.Combine(workDirectory, $"{testName}", $"{testName}_{currentDate}");
            string screenshotName = $"{TestContext.CurrentContext.Test.Name}_{currentDate}.png";

            try
            {
                // Ensure screenshot folder exists
                if (!Directory.Exists(screenshotPath))
                {
                    Directory.CreateDirectory(screenshotPath);
                }

                string fullScreenshotFilePath = Path.Combine(screenshotPath, screenshotName);

                // Take screenshot and save to file
                var screenshotBytes = await TakeScreenshotHelper(fullScreenshotFilePath, FULL_PAGE_SCREENSHOT);

                // Save a copy to Allure results directory and get relative path
                string allureFileName = Guid.NewGuid().ToString() + ".png";
                string allureFilePath = Path.Combine(AllureLifecycle.Instance.ResultsDirectory, allureFileName);
                File.WriteAllBytes(allureFilePath, screenshotBytes);

                AllureLifecycle.Instance.UpdateTestCase(test =>
                {
                    test.attachments.Add(new Attachment
                    {
                        name = screenshotName,
                        type = "image/png",
                        source = allureFileName
                    });
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error taking screenshot: {ex.Message}", ex);
            }
        }

        private async Task<byte[]> TakeScreenshotHelper(string path, bool fullPage)
        {
            return await _page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = path,
                FullPage = fullPage
            });
        }
    }
}
