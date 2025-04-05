using Microsoft.Playwright;

namespace PlaywrightTestSolution.BusinessLogic.Helpers
{
    public class Waiters
    {
        private IPage _page;
        private const int TIMEOUT = 10000; // 10 seconds
        private const int STEP = 500; // 0.5 seconds

        public Waiters(IPage page)
        {
            _page = page;
        }

        public async Task WaitForElementToBeVisible(ILocator locator)
        {
            await locator.WaitForAsync(new LocatorWaitForOptions()
            {
                State = WaitForSelectorState.Visible,
                Timeout = TIMEOUT
            });
        }
        public async Task WaitForElementToBeHidden(ILocator locator)
        {
            await locator.WaitForAsync(new LocatorWaitForOptions()
            {
                State = WaitForSelectorState.Hidden,
                Timeout = TIMEOUT
            });
        }

        public static async Task WaitForElementToBeVisible(ILocator locator, int timeout = TIMEOUT, int step = STEP, string? logMessage = null)
        {
            await CoreWaitForCondition(() => locator.IsVisibleAsync(), timeout, step, logMessage);
        }

        public static async Task WaitForElementToBeDisabled(ILocator locator, int timeout = TIMEOUT, int step = STEP, string? logMessage = null)
        {
            await CoreWaitForCondition(() => locator.IsDisabledAsync(), timeout, step, logMessage);
        }

        public static async Task WaitForElementToBeEnabled(ILocator locator, int timeout = TIMEOUT, int step = STEP, string? logMessage = null)
        {
            await CoreWaitForCondition(() => locator.IsEnabledAsync(), timeout, step, logMessage);
        }

        // For async methods
        public static Task WaitForCondition(Func<Task<bool>> condition, int timeout = TIMEOUT, int step = STEP, string? logMessage = null)
        {
            // Verify that time data can be used
            if (timeout < 0 || timeout < step || step <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout), "Timeout/Step has incorrect value.");
            }
            return CoreWaitForCondition(condition, timeout, step, logMessage);
        }

        // For sync methods
        public static Task WaitForCondition(Func<bool> condition, int timeout = TIMEOUT, int step = STEP, string? logMessage = null)
        {
            // Verify that time data can be used
            if (timeout < 0 || timeout < step || step <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout), "Timeout/Step has incorrect value.");
            }
            return WaitForCondition(() => Task.FromResult(condition()), timeout, step, logMessage);
        }

        private static async Task CoreWaitForCondition(Func<Task<bool>> condition, int timeout, int step, string? logMessage = null)
        {            
            var startTime = DateTime.Now;
            while (true)
            {
                if (await condition())
                {
                    return;
                }
                if ((DateTime.Now - startTime).TotalMilliseconds > timeout)
                {
                    throw new TimeoutException($"Condition not met within the specified timeout {timeout}");
                }
                await Task.Delay(step);
            }
        }
    }
}
