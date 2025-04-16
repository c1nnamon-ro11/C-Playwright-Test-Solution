namespace PlaywrightTestSolution.BusinessLogic.Helpers.Users.Strategies
{
    public class RoleFilterStrategy : IUserFilterStrategy
    {
        private static readonly Random random = new();

        public UserModel Filter(List<UserModel> users, string role)
        {
            var matchingUsers = users.Where(user => user.Role == role).ToList();
            if (matchingUsers.Count == 0)
            {
                throw new InvalidOperationException($"No users found with Role: {role}");
            }
            return matchingUsers[random.Next(matchingUsers.Count)];
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
