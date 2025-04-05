using PlaywrightTestSolution.BusinessLogic.Actions;
using PlaywrightTestSolution.BusinessLogic.Drivers;
using PlaywrightTestSolution.BusinessLogic.Helpers;
using PlaywrightTestSolution.Connectors.Locators.AdvancedInteractions;

namespace PlaywrightTestSolution.BusinessLogic.PageObjects.Pages.AdvancedInteractions
{
    public class AdobeConverterPage
    {
        private readonly Driver _driver;
        private readonly BaseActions _baseActions;
        private AdobeConverterPageLocators _adobeConverterPageLocators;

        private const string _adobeConverterPageURL = "https://www.adobe.com/ua/acrobat/online/pdf-to-word.html";

        public AdobeConverterPage(Driver driver)
        {
            _driver = driver;
            _baseActions = new BaseActions(_driver);
            _adobeConverterPageLocators = new AdobeConverterPageLocators(_driver.Page);
        }

        public async Task NavigateTo() => await _baseActions.NavigateTo(_adobeConverterPageURL);

        public async Task WaitForPageToLoad()
        {
            await Waiters.WaitForElementToBeVisible(_adobeConverterPageLocators.DcConverterWidget);
        }

        public async Task<bool> IsQuestionListOpened(int listItemIndex)
        {
            string? classValue = await _baseActions.GetAttribute(_adobeConverterPageLocators.QuestionList(listItemIndex), "daa-ll");
            return classValue?.Contains("close") ?? false;
        }

        public async Task<bool> IsQuestionListAnswerOpened(int listItemIndex) 
            => !await _baseActions.IsHidden(_adobeConverterPageLocators.QuestionListAnswer(listItemIndex));

         public async Task OpenListAnswer(int listItemIndex) => await _baseActions.Click(_adobeConverterPageLocators.QuestionList(listItemIndex));

        public async Task UploadDocumentFromSystem(string name)
        {
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", name);
            await _baseActions.UploadFile(_adobeConverterPageLocators.FileInput, fullPath);
        }
        
        public async Task<bool> IsReadyToBeDownloaded() => await _baseActions.IsVisible(_adobeConverterPageLocators.DownloadButton);

        public async Task<string> GetFileDropzoneBorderColor() => await _baseActions.GetBackgroundBorderColor(_adobeConverterPageLocators.ContentWrapper);
        public async Task HoverOnFileDropzone() => await _baseActions.Hover(_adobeConverterPageLocators.ContentWrapper);
    }
}

