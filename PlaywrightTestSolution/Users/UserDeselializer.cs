using System.Text.Json;

namespace PlaywrightTestSolution.Users
{
    public class UserDeselializer
    {
        const string usersFileName = @"\Users\Users.json";

        public static List<UserModel> GetUsers()
        {
            string path = Directory.GetCurrentDirectory();
            string usersJsonString = File.ReadAllText(path + usersFileName);
            List<UserModel>? users = JsonSerializer.Deserialize<List<UserModel>>(json: usersJsonString);
            return users ?? new List<UserModel>();
        }
    }
}
