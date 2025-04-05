using System.Text.Json;

namespace PlaywrightTestSolution.BusinessLogic.Helpers.Users
{
    public class UserDeselializer
    {
        const string usersFileName = @"\BusinessLOgic\Helpers\Users\Users.json";

        public static List<UserModel> GetUsers()
        {
            string path = Directory.GetCurrentDirectory();
            string usersJsonString = File.ReadAllText(path + usersFileName);
            List<UserModel>? users = JsonSerializer.Deserialize<List<UserModel>>(json: usersJsonString);
            return users ?? new List<UserModel>();
        }
    }
}
