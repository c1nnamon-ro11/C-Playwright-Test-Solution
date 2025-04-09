using Microsoft.Playwright;
using PlaywrightTestSolution.BusinessLogic.Actions;
using PlaywrightTestSolution.BusinessLogic.Drivers;
using PlaywrightTestSolution.BusinessLogic.Helpers;
using PlaywrightTestSolution.Connectors.Locators.AdvancedInteractions;
using Serilog;

namespace PlaywrightTestSolution.BusinessLogic.PageObjects.Pages.AdvancedInteractions
{
    public class GlobalSQAPage
    {
        private readonly Driver _driver;
        private readonly BaseActions _baseActions;
        private GlobalSQAPageLocators _globalSQAPageLocators;
        private ILogger _logger;

        private const string GLOBAL_SQA_PAGE_URL = "https://www.globalsqa.com/demo-site/draganddrop/";
        public GlobalSQAPage(Driver driver, ILogger logger)
        {
            _driver = driver;
            _logger = logger;
            _baseActions = new BaseActions(_driver, _logger);
            _globalSQAPageLocators = new GlobalSQAPageLocators(_driver.Page);
        }

        public async Task NavigateTo()
        {
            _logger.Debug($"Navigate to test page.");
            await _baseActions.NavigateTo(GLOBAL_SQA_PAGE_URL);
        }

        public async Task WaitForPageToLoad()
        {
            _logger.Debug($"Wait for page to be loaded.");
            await Waiters.WaitForElementToBeVisible(_globalSQAPageLocators.MainHeader);
        }

        public async Task MoveItemToTrashBin(int galleryItemIndex)
        {
            _logger.Debug($"Move item to trash bin.");
            await _baseActions.DragAndDrop(_globalSQAPageLocators.GalleryItem(galleryItemIndex), _globalSQAPageLocators.TrashBin);
        }

        public async Task<bool> IsItemInDefaultList(int galleryItemIndex)
        {
            _logger.Debug($"Check if item is in default list.");
            return await _baseActions.IsVisible(_globalSQAPageLocators.GalleryItem(galleryItemIndex));
        }

        public async Task<bool> IsItemInTrashList(int galleryItemIndex)
        {
            _logger.Debug($"Check if item is in trash bin.");
            return await _baseActions.IsVisible(_globalSQAPageLocators.GalleryItemInTrash(galleryItemIndex));
        }

        public async Task<bool> IsTrahsBinContainsAnyItem() => await _baseActions.IsVisible(_globalSQAPageLocators.TrashBinContent);
    }
}


