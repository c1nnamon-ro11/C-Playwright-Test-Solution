using PlaywrightTestSolution.BusinessLogic.Actions;
using PlaywrightTestSolution.BusinessLogic.Drivers;
using PlaywrightTestSolution.BusinessLogic.Helpers;
using PlaywrightTestSolution.Connectors.Locators.AdvancedInteractions;
using Serilog;

namespace PlaywrightTestSolution.BusinessLogic.PageObjects.Pages.AdvancedInteractions
{
    public class AdobeConverterPage
    {
        private readonly Driver _driver;
        private readonly BaseActions _baseActions;
        private AdobeConverterPageLocators _adobeConverterPageLocators;
        private ILogger _logger;

        private const string ADOBE_CONBERTER_PAGE_URL = "https://www.adobe.com/ua/acrobat/online/pdf-to-word.html";

        public AdobeConverterPage(Driver driver, ILogger logger)
        {
            _driver = driver;
            _logger = logger;
            _baseActions = new BaseActions(_driver, _logger);
            _adobeConverterPageLocators = new AdobeConverterPageLocators(_driver.Page);
        }

        public async Task NavigateTo()
        {
            _logger.Debug($"Navigate to test page.");
            await _baseActions.NavigateTo(ADOBE_CONBERTER_PAGE_URL);
        }

        public async Task WaitForPageToLoad()
        {
            _logger.Debug($"Wait for page to be loaded.");
            await Waiters.WaitForElementToBeVisible(_adobeConverterPageLocators.DcConverterWidget);
        }

        public async Task<bool> IsQuestionListOpened(int listItemIndex)
        {
            _logger.Debug($"Check if expected list in opened state.");
            string? classValue = await _baseActions.GetAttribute(_adobeConverterPageLocators.QuestionList(listItemIndex), "daa-ll");
            return classValue?.Contains("close") ?? false;
        }

        public async Task<bool> IsQuestionListAnswerOpened(int listItemIndex)
        {
            _logger.Debug($"Check if expected answer opened.");
            return !await _baseActions.IsHidden(_adobeConverterPageLocators.QuestionListAnswer(listItemIndex));
        } 

         public async Task OpenListAnswer(int listItemIndex)
         {
            _logger.Debug($"Open one of lists (by index - {listItemIndex}).");
            await _baseActions.Click(_adobeConverterPageLocators.QuestionList(listItemIndex));
         }

        public async Task UploadDocumentFromSystem(string name)
        {
            _logger.Debug($"Upload {name} document.");
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Tests", "TestData", name);
            await _baseActions.UploadFile(_adobeConverterPageLocators.FileInput, fullPath);
        }
        
        public async Task<bool> IsReadyToBeDownloaded()
        {
            _logger.Debug($"Check if download button is present so document can be downloaded.");
            return await _baseActions.IsVisible(_adobeConverterPageLocators.DownloadButton);
        }

        public async Task<string> GetFileDropzoneBorderColor()
        {
            _logger.Debug($"Get dropzone border color");
            return await _baseActions.GetBackgroundBorderColor(_adobeConverterPageLocators.ContentWrapper);
        } 
        public async Task HoverOnFileDropzone()
        {
            _logger.Debug($"Hover on file dropzone.");
            await _baseActions.Hover(_adobeConverterPageLocators.ContentWrapper);
        } 
    }
}

