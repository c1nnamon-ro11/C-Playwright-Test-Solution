using PlaywrightTestSolution.BusinessLogic.PageObjects.Pages.AdvancedInteractions;
using PlaywrightTestSolution.BusinessLogic.Helpers;
using PlaywrightTestSolution.BusinessLogic.Actions;

namespace PlaywrightTestSolution.Tests.PlayTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]   
    public class AdvancedInteractionsTests : BaseTest
    {
        private BaseActions _baseActions;
        private AdobeConverterPage _adobeConverterPage;

        [SetUp]
        public async Task Setup()
        {
            _baseActions = new BaseActions(driver, logger!);
            _adobeConverterPage = new AdobeConverterPage(driver, logger!);

            await _adobeConverterPage.NavigateTo();
            await _adobeConverterPage.WaitForPageToLoad();
        }

        // Click tabs till openening expecting element
        [Test]
        public async Task PressingTab()
        {
            const int EXPECTED_LIST_ITEM_TO_BE_HIGHLIGHTED = 1;
            const int EXPECTED_LIST_ITEM_TO_NOT_BE_HIGHLIGHTED = 2;
            const int EXPECTED_NUMBER_OF_TABS = 19;

            // Pressing Tab till expected item will be selected
            logger!.Information($"Pressing Tab till expected item will be selected.");
            for (int i = 0; i <= EXPECTED_NUMBER_OF_TABS; i++)
            {
                await Task.Delay(200);
                await _baseActions.PressKey("Tab");
            }

            logger.Information($"Press Enter till and Verify that expected item selected.");
            await _baseActions.PressKey("Enter");
            CustomAssertions.BeTrue(await _adobeConverterPage.IsQuestionListOpened(EXPECTED_LIST_ITEM_TO_BE_HIGHLIGHTED), 
                $"Verify that {EXPECTED_LIST_ITEM_TO_BE_HIGHLIGHTED} list is opened");
            CustomAssertions.BeFalse(await _adobeConverterPage.IsQuestionListOpened(EXPECTED_LIST_ITEM_TO_NOT_BE_HIGHLIGHTED),
                $"Verify that {EXPECTED_LIST_ITEM_TO_NOT_BE_HIGHLIGHTED} list is not opened");
        }

        // Open dropdown with text
        // Refresh page
        // Verify that dropdown was closed
        [Test]
        public async Task RefreshingPage()
        {
            const int EXPECTED_LIST_ITEM = 1;

            CustomAssertions.BeFalse(await _adobeConverterPage.IsQuestionListAnswerOpened(EXPECTED_LIST_ITEM), 
                $"{EXPECTED_LIST_ITEM} list item is not opened.");

            // Open list item
            await _adobeConverterPage.OpenListAnswer(EXPECTED_LIST_ITEM);
            CustomAssertions.BeTrue(await _adobeConverterPage.IsQuestionListAnswerOpened(EXPECTED_LIST_ITEM),
                $"{EXPECTED_LIST_ITEM} list item is opened.");

            // Refresh page
            await _baseActions.Refresh();
            await _adobeConverterPage.WaitForPageToLoad();

            // Verify that item again hidden
                        CustomAssertions.BeFalse(await _adobeConverterPage.IsQuestionListAnswerOpened(EXPECTED_LIST_ITEM), 
                $"{EXPECTED_LIST_ITEM} list item is not opened again.");
        }

        // Upload test document to input by selecting file
        [Test]
        public async Task UploadingDocumentFromSystem()
        {
            string DOCUMENT_NAME = "TestDocument.pdf";

            await _adobeConverterPage.UploadDocumentFromSystem(DOCUMENT_NAME);
            await Waiters.WaitForCondition(async () => await _adobeConverterPage.IsReadyToBeDownloaded());
        }

        // Get element border color
        // Hover on test elemet
        // Verify that border color was updated 
        [Test]
        public async Task HoverMouseOnButton()
        {
            string DEFAULT_DROPZONE_BACKGROUND_COLOR = "rgb(213, 213, 213)";
            string HOVERED_DROPZONE_BACKGROUND_COLOR = "rgb(177, 177, 177)";

            CustomAssertions.Be(await _adobeConverterPage.GetFileDropzoneBorderColor(), DEFAULT_DROPZONE_BACKGROUND_COLOR, 
                $"Dropzone border has expected {DEFAULT_DROPZONE_BACKGROUND_COLOR} border color");
            await _adobeConverterPage.HoverOnFileDropzone();
            CustomAssertions.Be(await _adobeConverterPage.GetFileDropzoneBorderColor(), HOVERED_DROPZONE_BACKGROUND_COLOR,
                $"Dropzone border has expected {HOVERED_DROPZONE_BACKGROUND_COLOR} border color after hovering");
        }
    }
}