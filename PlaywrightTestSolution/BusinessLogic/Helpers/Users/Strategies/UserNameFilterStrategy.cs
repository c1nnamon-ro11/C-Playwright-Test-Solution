namespace PlaywrightTestSolution.BusinessLogic.Helpers.Users.Strategies
{
    public class UserNameFilterStrategy : IUserFilterStrategy
    {
        public UserModel Filter(List<UserModel> users, string userName)
        {
            return users.FirstOrDefault(user => user.UserName == userName)
                ?? throw new InvalidOperationException($"No user found with UserName: {userName}");
        }

        public UserModel Filter(List<UserModel> users, string parameter, string value)
        {
            throw new NotSupportedException("Filtering by parameter and value is not supported in UserNameFilterStrategy.");
        }

        public UserModel Filter(List<UserModel> users, Dictionary<string, string> filters)
        {
            throw new NotSupportedException("Filtering by multiple parameters is not supported in UserNameFilterStrategy.");
        }
    }
}
