using System;
using Microsoft.Playwright;
using PlaywrightTestSolution.Drivers;

namespace PlaywrightTestSolution.Actions
{
    public class BaseActions
    {
        private readonly Driver _driver;
        public BaseActions(Driver driver) 
        {
            _driver = driver;
        }

        public async Task NavigateTo(string targetURL) => await _driver.Page.GotoAsync(targetURL);

        public string GetCurrentURL() => _driver.Page.Url;

        public async Task SetInput(ILocator locator, string textInput, bool clearInput = false)
        {
            if (clearInput)
            {
                await locator.ClearAsync();
            }
            await locator.FillAsync(textInput);
        }

        public async Task Click(ILocator locator) => await locator.ClickAsync();

        public async Task<string> GetText(ILocator locator) => await locator.InnerTextAsync();

        public async Task<List<string>> GetElementsList(ILocator locator)
        {
            var elements = await locator.AllAsync();
            List<string> elementsText = new List<string>();
            foreach (var element in elements)
            {
                elementsText.Add(await element.InnerTextAsync());
            }
            return elementsText;
        }
    }
}
