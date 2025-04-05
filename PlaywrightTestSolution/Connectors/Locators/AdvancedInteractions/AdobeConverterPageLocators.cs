using Microsoft.Playwright;

namespace PlaywrightTestSolution.Connectors.Locators.AdvancedInteractions
{
    public class AdobeConverterPageLocators
    {
        private IPage _page;
        public AdobeConverterPageLocators(IPage page)
        {
            _page = page;
        }

        public ILocator DcConverterWidget => _page.Locator("//div[@data-test-id='dropzone-footer-block']");
        public ILocator FileInput => _page.Locator("//*[@id='fileInput' and contains(@class, 'FileUpload')]");
        public ILocator DownloadButton => _page.Locator("//button[text()='Download']");
        public ILocator QuestionList(int listIndex) => _page.Locator($"//div[@id='accordion-1']//button[@id='accordion-1-trigger-{listIndex}']");
        public ILocator QuestionListAnswer(int listIndex) => _page.Locator($"//div[@id='accordion-1-content-{listIndex}']");
        public ILocator ContentWrapper => _page.Locator("//div[contains(@class, 'LifecycleDropZone') and contains(@class, 'ContentWrapper')]");
    }
}

