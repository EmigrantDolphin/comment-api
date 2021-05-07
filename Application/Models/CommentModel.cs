using System;

namespace Application.Models
{
    public class CommentModel
    {
        public Guid CommentId {get; set;}
        public string Comment {get; set;}
        public string AuthorFullName {get; set;}
        public bool CanEdit {get; set;}
    }
}