using System;
using Domain.Entities;

namespace Application.Models
{
    public class UserModel
    {
        public Guid UserId {get; set;}
        public string Username {get; set;}
        public string FullName {get; set;}

        public static UserModel ToModel(User user) =>
            new UserModel
            {
                UserId = user.UserId,
                Username = user.Username,
                FullName = user.FullName
            };
    }
}