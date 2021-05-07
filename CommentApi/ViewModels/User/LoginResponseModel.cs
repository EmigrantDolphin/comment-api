using System;

namespace CommentApi.ViewModels.User
{
    public class LoginResponseModel
    {
        public Guid UserId {get; set;}
        public string Username {get; set;}
        public string Token {get; set;}
    }
}