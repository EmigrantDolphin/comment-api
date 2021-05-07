using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User 
    {
        public Guid UserId {get; private set;}
        public string Username {get; private set;}
        public string Password {get; private set;}
        public string FullName {get; private set;}

        public IEnumerable<Comment> Comments {get; private set;}

        public User(string username, string password, string fullName)
        {
            Username = username;
            Password = password;
            FullName = fullName;
        }
    }
}