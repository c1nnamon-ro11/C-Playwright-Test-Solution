using System.Text.Json;

namespace PlaywrightTestSolution.BusinessLogic.Helpers.Users
{
    public class UserDeselializer
    {
        const string usersFileName = @"\BusinessLOgic\Helpers\Users\Users.json";
        static Random random = new Random();

        private static List<UserModel> GetUsers()
        {
            string path = Directory.GetCurrentDirectory();
            string usersJsonString = File.ReadAllText(path + usersFileName);
            List<UserModel>? users = JsonSerializer.Deserialize<List<UserModel>>(json: usersJsonString);
            return users ?? new List<UserModel>();
        }

        public static UserModel GetUserByEmail(string username)
        {
            var users = GetUsers();
            var targetUser = users.Select(user => user).Where(user => user.UserName == username).First();
            return targetUser;
        }

        // Role, Viewer
        public static UserModel GetUserByParameter(string parameterAttribute, string parameterValue)
        {
            var users = GetUsers();
            var property = typeof(UserModel).GetProperty(parameterAttribute);

            var targetUsers = users.Select(user => user).Where(user =>
            {
                var propertyValueFromUser = property!.GetValue(user)?.ToString();
                return propertyValueFromUser == parameterValue;
            }).ToList();

            if (targetUsers.Count == 0)
                throw new InvalidOperationException($"No users found with {parameterAttribute} = {parameterValue}");

            return targetUsers[random.Next(targetUsers.Count)];
        }
    }
}
