using Microsoft.Playwright;

namespace PlaywrightTestSolution.Connectors.Locators.AdvancedInteractions
{
    public class GlobalSQAPageLocators
    {
        private IPage _page;
        public GlobalSQAPageLocators(IPage page)
        {
            _page = page;
        }

        public ILocator MainHeader => _page.Locator("//h1[text()='Drag And Drop']");
        public ILocator IFrame => _page.Locator("//div[@rel-title='Photo Manager']/p/iframe");
        public ILocator TrashBin => IFrame.ContentFrame.Locator("#trash");
        public ILocator GalleryItem(int index) => IFrame.ContentFrame.Locator($"//*[@id='gallery']/li[{index}]");
        public ILocator GalleryItemInTrash(int index) => IFrame.ContentFrame.Locator($"//*[@id='trash']//li[{index}]");
        public ILocator TrashBinContent => IFrame.ContentFrame.Locator("(//*[@id='trash']//ul/*)[1]");
    }
}

