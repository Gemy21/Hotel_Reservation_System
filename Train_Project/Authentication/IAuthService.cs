namespace Train_Project.Authentication
{
    public interface IAuthService
    {
        string GenerateToken(int userId, string userName, string role);

    }
}
