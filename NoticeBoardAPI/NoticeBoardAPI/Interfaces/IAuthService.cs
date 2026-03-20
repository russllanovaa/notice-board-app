namespace NoticeBoardAPI.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(int userId, string email, string displayName);
        bool IsTokenBlacklisted(string token);
        void BlacklistToken(string token);
    }
}
