namespace PlaywrightTestSolution.BusinessLogic.Helpers.Users.Strategies
{
    public interface IUserFilterStrategy
    {
        UserModel Filter(List<UserModel> users, string parameter);
        UserModel Filter(List<UserModel> users, string parameter, string value);
        UserModel Filter(List<UserModel> users, Dictionary<string, string> filters);
    }
}
