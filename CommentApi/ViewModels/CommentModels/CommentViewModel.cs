using System;
using System.Linq;
using System.Collections.Generic;

using Application.Models;

namespace CommentApi.ViewModels.CommentModels
{
    public class CommentViewModel : CommentModel
    {
        public Guid CommentId {get; set;}
        public string AuthorFullName {get; set;}
        public bool CanEdit {get; set;}

        public static List<CommentViewModel> ToViewModels(List<Application.Models.CommentModel> comments)
        {
            return comments.Select(x => new CommentViewModel
            {
                CommentId = x.CommentId,
                Comment = x.Comment,
                AuthorFullName = x.AuthorFullName,
                CanEdit = x.CanEdit
            }).ToList();
        }
    }
}