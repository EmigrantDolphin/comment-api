using System.ComponentModel.DataAnnotations;

namespace CommentApi.ViewModels.User
{
    public class LoginModel
    {
        [Required]
        public string Username {get; set;}
        [Required]
        public string Password {get; set;}
    }
}