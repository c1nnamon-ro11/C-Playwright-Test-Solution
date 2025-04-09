using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;

namespace PlaywrightTestSolution.Tests.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomRetry : NUnitAttribute, IWrapSetUpTearDown
    {
        private readonly int _maxRetries;
        private readonly int _delayMilliseconds;

        public CustomRetry(int maxRetries, int delayMilliseconds = 5000)
        {
            _maxRetries = maxRetries;
            _delayMilliseconds = delayMilliseconds;
        }

        public TestCommand Wrap(TestCommand command)
        {
            return new RetryWithDelayCommand(command, _maxRetries, _delayMilliseconds);
        }

        private sealed class RetryWithDelayCommand : DelegatingTestCommand
        {
            private readonly int _maxRetries;
            private readonly int _delayMilliseconds;

            public RetryWithDelayCommand(TestCommand innerCommand, int maxRetries, int delayMilliseconds = 5000)
                : base(innerCommand)
            {
                _maxRetries = maxRetries;
                _delayMilliseconds = delayMilliseconds;
            }

            public override TestResult Execute(TestExecutionContext context)
            {
                int attempt = 1;
                string originalTestName = context.CurrentTest.Name;

                while (attempt <= _maxRetries)
                {
                    try
                    {
                        var task = Task.Run(() => innerCommand.Execute(context));
                        if (!task.Wait(context.TestCaseTimeout))
                            throw new TimeoutException($"Test execution timed out after {TimeSpan.FromMilliseconds(context.TestCaseTimeout)} milliseconds.");
                    }
                    catch (Exception ex)
                    {
                        context.CurrentResult.SetResult(ResultState.Failure, ex.Message);
                    }
                    if (context.CurrentResult.ResultState == ResultState.Success)
                    {
                        return context.CurrentResult;
                    }
                    else
                    {
                        attempt++;
                        Task.Delay(_delayMilliseconds).Wait();
                        if (attempt <= _maxRetries)
                        {
                            context.CurrentResult = context.CurrentTest.MakeTestResult(); // Reset if there will be new attempt
                        }
                    }
                }
                return context.CurrentResult;
            }
        }
    }   
}