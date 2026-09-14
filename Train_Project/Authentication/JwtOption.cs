namespace Train_Project.Authentication
{
    public class JwtOption
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Lifetime { get; set; }
        public string SignKey { get; set; }
    }
}
