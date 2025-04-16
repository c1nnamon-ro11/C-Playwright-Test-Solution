using System;

namespace PlaywrightTestSolution.BusinessLogic.Helpers.Users.Strategies
{
    public class DefaultFilterStrategy : IUserFilterStrategy
    {
        private static readonly Random random = new();
        public UserModel Filter(List<UserModel> users, string parameter)
        {
            throw new NotSupportedException("Filtering by parameter is not supported in DefaultFilterStrategy.");
        }

        public UserModel Filter(List<UserModel> users, string parameter, string value)
        {
            var property = typeof(UserModel).GetProperty(parameter);
            if (property == null)
                throw new ArgumentException($"Property '{value}' does not exist on UserModel.");

            var targetUsers = users.Where(user =>
            {
                var usersValue = property.GetValue(user)?.ToString();
                return usersValue == value;
            }).ToList();

            if (targetUsers.Count == 0)
                throw new InvalidOperationException($"No users found with {parameter} = {value}");

            return targetUsers[random.Next(targetUsers.Count)];
        }

        public UserModel Filter(List<UserModel> users, Dictionary<string, string> filters)
        {
            var filteredUsers = users;
            foreach (var filter in filters)
            {
                var property = typeof(UserModel).GetProperty(filter.Key);
                if (property == null)
                    throw new ArgumentException($"Property '{filter.Key}' does not exist on UserModel.");
                filteredUsers = [.. filteredUsers.Where(user =>
                {
                    var userValue = property.GetValue(user)?.ToString();
                    return userValue == filter.Value;
                })];
            }
            if (filteredUsers.Count == 0)
                throw new InvalidOperationException("No users found with the specified filters.");

            return filteredUsers[random.Next(filteredUsers.Count)];
        }
    }
}
