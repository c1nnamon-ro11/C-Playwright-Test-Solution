using PlaywrightTestSolution.BusinessLogic.Helpers.Users.Strategies;

namespace PlaywrightTestSolution.BusinessLogic.Helpers.Users
{
    public class UserFilterContext
    {
        private readonly IUserFilterStrategy _strategy;

        public UserFilterContext(IUserFilterStrategy strategy)
        {
            _strategy = strategy;
        }

        public UserModel ExecuteFilter(List<UserModel> users, string parameter)
        {
            return _strategy.Filter(users, parameter);
        }

        public UserModel ExecuteFilter(List<UserModel> users, string parameter, string value)
        {
            return _strategy.Filter(users, parameter, value);
        }

        public UserModel ExecuteFilter(List<UserModel> users, Dictionary<string, string> filters)
        {
            return _strategy.Filter(users, filters);
        }
    }
}
