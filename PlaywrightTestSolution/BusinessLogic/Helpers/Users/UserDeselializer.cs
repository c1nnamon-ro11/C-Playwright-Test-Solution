using System.Text.Json;
using PlaywrightTestSolution.BusinessLogic.Helpers.Users.Strategies;

namespace PlaywrightTestSolution.BusinessLogic.Helpers.Users
{
    public class UserDeselializer
    {
        const string usersFileName = @"\BusinessLogic\Helpers\Users\Users.json";

        private static List<UserModel> GetUsers()
        {
            string path = Directory.GetCurrentDirectory();
            string usersJsonString = File.ReadAllText(path + usersFileName);
            List<UserModel>? users = JsonSerializer.Deserialize<List<UserModel>>(json: usersJsonString);
            return users ?? [];
        }

        public static UserModel GetUserByEmail(string email)
        {
            var users = GetUsers();
            var context = new UserFilterContext(new EmailFilterStrategy());
            return context.ExecuteFilter(users, email);
        }

        public static UserModel GetUserByRole(string role)
        {
            var users = GetUsers();
            var context = new UserFilterContext(new RoleFilterStrategy());
            return context.ExecuteFilter(users, role);
        }

        public static UserModel GetUserByUsername(string username)
        {
            var users = GetUsers();
            var context = new UserFilterContext(new UserNameFilterStrategy());
            return context.ExecuteFilter(users, username);
        }

        public static UserModel GetUserByFilter(string parameter, string value)
        {
            var users = GetUsers();
            var context = new UserFilterContext(new DefaultFilterStrategy());
            return context.ExecuteFilter(users, parameter, value);
        }

        public static UserModel GetUserByMultipleFilters(Dictionary<string, string> filters)
        {
            var users = GetUsers();
            var context = new UserFilterContext(new DefaultFilterStrategy());
            return context.ExecuteFilter(users, filters);
        }       
    }
}
