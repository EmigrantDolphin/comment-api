using System;

namespace Domain.Entities
{
    public class Comment 
    {
        public Guid CommentId {get; private set;}
        public string Value {get; private set;}
        public DateTimeOffset CreatedOn {get; private set;}

        public Guid UserId {get; private set;}
        public User User {get; private set;}



        public Comment(string value, Guid userId){
            Value = value;
            UserId = userId;
            CreatedOn = DateTimeOffset.Now;
        }
    }
}