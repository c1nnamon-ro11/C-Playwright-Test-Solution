using PlaywrightTestSolution.BusinessLogic.PageObjects.Pages.AdvancedInteractions;
using PlaywrightTestSolution.BusinessLogic.Helpers;
using PlaywrightTestSolution.BusinessLogic.Actions;

namespace PlaywrightTestSolution.Tests.PlayTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class AdvancedInteractionsTests2 : BaseTest
    {
        private BaseActions _baseActions;
        private GlobalSQAPage _globalSQAPage;

        [SetUp]
        public async Task Setup()
        {
            _baseActions = new BaseActions(driver, logger!);
            _globalSQAPage = new GlobalSQAPage(driver, logger!);

            await _globalSQAPage.NavigateTo();
            await _globalSQAPage.WaitForPageToLoad();
        }

        // DragAndDrop item from default list to trash bin
        // Verify item present in trash bin but not in default list
        [Test]
        public async Task DragAndDropAction()
        {
            const int TEST_ITEM_INDEX = 1;
            const int TEST_ITEM_INDEX_2 = 3;

            logger!.Information("Verify that items presented in default container");
            var isElementVisible = await _globalSQAPage.IsItemInDefaultList(TEST_ITEM_INDEX);
            CustomAssertions.BeTrue(await _globalSQAPage.IsItemInDefaultList(TEST_ITEM_INDEX),
                $"Item {TEST_ITEM_INDEX} is present in default list");
            CustomAssertions.BeTrue(await _globalSQAPage.IsItemInDefaultList(TEST_ITEM_INDEX_2),
               $"Item {TEST_ITEM_INDEX_2} is present in default list");

            logger!.Information("Drop one item to trash bin");
            await _globalSQAPage.MoveItemToTrashBin(TEST_ITEM_INDEX);
            await Waiters.WaitForCondition(async () =>  await _globalSQAPage.IsTrahsBinContainsAnyItem());

            logger!.Information("Verify that only expected item was moved to trash bin");
            CustomAssertions.BeTrue(await _globalSQAPage.IsItemInTrashList(TEST_ITEM_INDEX),
                $"Item {TEST_ITEM_INDEX} is present in default list");
            CustomAssertions.BeTrue(await _globalSQAPage.IsItemInDefaultList(TEST_ITEM_INDEX_2),
               $"Item {TEST_ITEM_INDEX_2} is present in default list");
        }
    }
}
