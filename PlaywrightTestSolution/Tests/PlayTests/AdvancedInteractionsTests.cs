using FluentAssertions;
using PlaywrightTestSolution.BusinessLogic.Helpers;
using PlaywrightTestSolution.BusinessLogic.PageObjects.Pages.AdvancedInteractions;
using PlaywrightTestSolution.BusinessLogic.Actions;

namespace PlaywrightTestSolution.Tests.PlayTests
{
    public class AdvancedInteractionsTests : BaseTest
    {
        private BaseActions _baseActions;
        private AdobeConverterPage _adobeConverterPage;

        [OneTimeSetUp]
        public new void OneTimeSetUp()
        {
            _baseActions = new BaseActions(driver);
            _adobeConverterPage = new AdobeConverterPage(driver);
        }

        [SetUp]
        public async Task Setup()
        {
            await _adobeConverterPage.NavigateTo();
            await _adobeConverterPage.WaitForPageToLoad();
        }

        // Click tabs till openening expecting element
        [Test]
        public async Task PressingTab()
        {
            const int XPECTED_LIST_ITEM_TO_BE_HIGHLIGHTED = 1;
            const int EXPECTED_LIST_ITEM_TO_NOT_BE_HIGHLIGHTED = 2;
            const int EXPECTED_NUMBER_OF_TABS = 19;
           
            // Pressing Tab till expected item will be selected
            for (int i = 0; i <= EXPECTED_NUMBER_OF_TABS; i++)
            {
                await Task.Delay(200);
                await _baseActions.PressKey("Tab");
            }
            await _baseActions.PressKey("Enter");

            // Verify that expected item selected
            (await _adobeConverterPage.IsQuestionListOpened(XPECTED_LIST_ITEM_TO_BE_HIGHLIGHTED)).Should().BeTrue();
            (await _adobeConverterPage.IsQuestionListOpened(EXPECTED_LIST_ITEM_TO_NOT_BE_HIGHLIGHTED)).Should().BeFalse();
        }

        // Open dropdown with text
        // Refresh page
        // Verify that dropdown was closed
        [Test]
        public async Task RefreshingPage()
        {
            const int EXPECTED_LIST_ITEM = 1;

            (await _adobeConverterPage.IsQuestionListAnswerOpened(EXPECTED_LIST_ITEM)).Should().BeFalse();

            // Open list item
            await _adobeConverterPage.OpenListAnswer(EXPECTED_LIST_ITEM);
            (await _adobeConverterPage.IsQuestionListAnswerOpened(EXPECTED_LIST_ITEM)).Should().BeTrue();

            // Refresh page
            await _baseActions.Refresh();
            await _adobeConverterPage.WaitForPageToLoad();

            // Verify that item again hidden
            (await _adobeConverterPage.IsQuestionListAnswerOpened(EXPECTED_LIST_ITEM)).Should().BeFalse();
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

            (await _adobeConverterPage.GetFileDropzoneBorderColor()).Should().Be(DEFAULT_DROPZONE_BACKGROUND_COLOR);
            await _adobeConverterPage.HoverOnFileDropzone();
            (await _adobeConverterPage.GetFileDropzoneBorderColor()).Should().Be(HOVERED_DROPZONE_BACKGROUND_COLOR);
        }
    }
}

