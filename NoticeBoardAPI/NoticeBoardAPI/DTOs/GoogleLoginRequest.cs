using System.ComponentModel.DataAnnotations;

namespace NoticeBoardAPI.DTOs
{
    public class GoogleLoginRequest
    {
        [Required(ErrorMessage = "Google токен обов'язковий")]
        public string IdToken { get; set; } = string.Empty;
    }
}
