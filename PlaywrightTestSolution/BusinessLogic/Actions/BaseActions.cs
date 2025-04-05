using System;
using Microsoft.Playwright;
using PlaywrightTestSolution.BusinessLogic.Drivers;

namespace PlaywrightTestSolution.BusinessLogic.Actions
{
    public class BaseActions
    {
        private readonly Driver _driver;
        public BaseActions(Driver driver) 
        {
            _driver = driver;
        }

        // Page
        public async Task NavigateTo(string targetURL) => await _driver.Page.GotoAsync(targetURL);

        public string GetCurrentURL() => _driver.Page.Url;
        public async Task Refresh() => await _driver.Page.ReloadAsync();

        // Setters
        public async Task SetInput(ILocator locator, string textInput, bool clearInput = false)
        {
            if (clearInput)
            {
                await locator.ClearAsync();
                await Task.Delay(500);
            }
            await locator.FillAsync(textInput);
            await Task.Delay(500);
        }

        public async Task Click(ILocator locator) => await locator.ClickAsync();        

        public async Task PressKey(string key)
        {
            var keyboard = _driver.Page.Keyboard;
            await keyboard.PressAsync(key);
        }

        public async Task UploadFile(ILocator locator, string path) => await locator.SetInputFilesAsync(path);

        // Getters
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

        public async Task<string?> GetAttribute(ILocator locator, string attributeName) => await locator.GetAttributeAsync(attributeName);
        public async Task<string> GetBackgroundBorderColor(ILocator locator) => 
            await locator.EvaluateAsync<string>("element => window.getComputedStyle(element).borderColor");
        public async Task<bool> IsHidden(ILocator locator) => await locator.IsHiddenAsync();
        public async Task<bool> IsVisible(ILocator locator) => await locator.IsVisibleAsync();

        // Actions
        public async Task Hover(ILocator locator) => await locator.HoverAsync();
    }
}
