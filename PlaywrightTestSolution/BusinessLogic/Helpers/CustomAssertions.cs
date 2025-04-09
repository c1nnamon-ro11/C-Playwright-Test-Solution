using System.Collections;
using FluentAssertions;
using Serilog;

namespace PlaywrightTestSolution.BusinessLogic.Helpers
{
    public class CustomAssertions
    {
        private static ILogger? _logger;

        public static void InitializeLogger(ILogger logger)
        {
            _logger = logger;
        }

        public static void Be<TValue>(TValue actual, TValue expected, string message = "")
        {
            try
            {
                actual.Should().Be(expected, message);
                _logger!.Information($"Assert: {message} - passed.");
            }
            catch (Exception)
            {
                _logger!.Information($"Assert: {message} - failed.");
                Assert.Fail();
            }
        }

        public static void BeTrue(bool actual, string message = "")
        {
            Be(actual, true, message);
        }

        public static void BeFalse(bool actual, string message = "")
        {
            Be(actual, false, message);
        }

        public static void Contains<TValue>(TValue actual, object expected, string message = "")
        {
            try
            {
                if (actual is string actualString && expected is string expectedSubstring)
                {
                    actualString.Should().Contain(expectedSubstring, message);
                }
                else if (actual is IEnumerable actualCollection && expected != null)
                {
                    actualCollection.Cast<object>().Should().Contain(expected, message);
                }
                else
                {
                    Assert.Fail();
                }

                _logger!.Information($"Assert: {message} - passed.");
            }
            catch (Exception)
            {
                _logger!.Information($"Assert: {message} - failed.");
                Assert.Fail();
            }
        }
    }
}