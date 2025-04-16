namespace PlaywrightTestSolution.BusinessLogic.Helpers.Users.Strategies
{
    public class EmailFilterStrategy : IUserFilterStrategy
    {
        public UserModel Filter(List<UserModel> users, string email)
        {
            return users.FirstOrDefault(user => user.UserEmail == email)
                ?? throw new InvalidOperationException($"No user found with Email: {email}");
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
