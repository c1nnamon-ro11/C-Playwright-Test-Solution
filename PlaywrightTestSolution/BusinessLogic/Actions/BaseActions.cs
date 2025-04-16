using Microsoft.Playwright;
using PlaywrightTestSolution.BusinessLogic.Drivers;
using Serilog;

namespace PlaywrightTestSolution.BusinessLogic.Actions
{
    public class BaseActions
    {
        private readonly Driver _driver;
        private readonly ILogger _logger;
        public BaseActions(Driver driver, ILogger logger)
        {
            _driver = driver;
            _logger = logger;
        }

        // Page
        public async Task NavigateTo(string targetURL)
        {
            _logger.Verbose($"Call NavigateTo method with '{targetURL}' parameter");
            await _driver.Page.GotoAsync(targetURL);
        }

        public string GetCurrentURL()
        {
            _logger.Verbose("Call GetCurrentURL method");
            return _driver.Page.Url;
        }
        public async Task Refresh()
        {
            _logger.Verbose("Call Refresh method");
            await _driver.Page.ReloadAsync();
        }

        // Setters
        public async Task SetInput(ILocator locator, string textInput, bool clearInput = false)
        {
            _logger.Verbose("Call SetInput method");
            if (clearInput)
            {
                _logger.Verbose("   Call clearing method");
                await locator.ClearAsync();
                await Task.Delay(500);
            }
            _logger.Verbose($"   Call fill method with '{textInput}' parameter");
            await locator.FillAsync(textInput);
            await Task.Delay(500);
        }

        public async Task Click(ILocator locator)
        {
            _logger.Verbose("Call Click method");
            await locator.ClickAsync();
        }

        public async Task PressKey(string key)
        {
            _logger.Verbose($"Call PressKey method with '{key}' parameter");
            var keyboard = _driver.Page.Keyboard;
            await keyboard.PressAsync(key);
        }

        public async Task UploadFile(ILocator locator, string path)
        {
            _logger.Verbose($"Call UploadFile method with '{path}' parameter");
            await locator.SetInputFilesAsync(path);
        } 

        // Getters
        public async Task<string> GetText(ILocator locator)
        {
            _logger.Verbose($"Call GetText method");
            return await locator.InnerTextAsync();
        }

        public async Task<List<string>> GetElementsListTexts(ILocator locator)
        {
            _logger.Verbose($"Call GetElementsListTexts method");
            var elements = await locator.AllAsync();
            List<string> elementsText = [];
            foreach (var element in elements)
            {
                elementsText.Add(await element.InnerTextAsync());
            }
            return elementsText;
        }

        public async Task<string?> GetAttribute(ILocator locator, string attributeName)
        {
            _logger.Verbose($"Call GetAttribute method with '{attributeName}' paramter");
            return await locator.GetAttributeAsync(attributeName);
        }

        public async Task<string> GetBackgroundBorderColor(ILocator locator)
        {
            _logger.Verbose($"Call GetBackgroundBorderColor method");
            return await locator.EvaluateAsync<string>("element => window.getComputedStyle(element).borderColor");
        }
            
        public async Task<bool> IsHidden(ILocator locator)
        {
            _logger.Verbose($"Call IsHidden method");
            return await locator.IsHiddenAsync();
        }
        public async Task<bool> IsVisible(ILocator locator)
        {
            _logger.Verbose($"Call IsVisible method");
            return await locator.IsVisibleAsync();
        }

        // Actions
        public async Task Hover(ILocator locator)
        {
            _logger.Verbose($"Call Hover method");
            await locator.HoverAsync();
        }

        public async Task DoubleClick(ILocator locator)
        {
            _logger.Verbose($"Call DoubleClick method");
            await locator.DblClickAsync();
        }

        public async Task RightClick(ILocator locator)
        {
            _logger.Verbose($"Call DoubleClick method");
            await locator.ClickAsync(new() { Button = MouseButton.Right });
        }

        public async Task DragAndDrop(ILocator draggable, ILocator droppable)
        {
            _logger.Verbose($"Call DragAndDrop method");
            await draggable.DragToAsync(droppable);
        }
    }
}
