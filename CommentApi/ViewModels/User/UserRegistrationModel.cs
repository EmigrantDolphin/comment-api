using System;
using System.ComponentModel.DataAnnotations;

namespace CommentApi.ViewModels.User
{
    public class UserRegistrationModel
    {
        [Required]
        public string Username {get; set;}

        [Required]
        public string Password {get; set;}

        [Required]
        public string FullName {get; set;}
    }
}