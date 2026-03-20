namespace NoticeBoardAPI.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string GoogleId {  get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DisplayName {  get; set; } = string.Empty;    
        public string? AvatarUrl {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastLogindAt { get; set; }
        public bool IsAxtive { get; set; }
    }
}
