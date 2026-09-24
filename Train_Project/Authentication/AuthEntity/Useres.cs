using Train_Project.Entities;

namespace Train_Project.Authentication.AuthEntity
{
    public class Users
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Roles { get; set; } = null!;

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public virtual Customer? Customer { get; set; }
    }
}