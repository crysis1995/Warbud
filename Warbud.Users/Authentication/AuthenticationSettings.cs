namespace Warbud.Users.Authentication
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; }
        public int JwtExpireSeconds { get; set; }
        public string JwtIssuer { get; set; }
        public int ClockSkew { get; set; }
        
        public long TicksPerSecond = 10_000 * 1_000;
    }
}