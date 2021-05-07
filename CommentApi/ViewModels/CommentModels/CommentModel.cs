using System.ComponentModel.DataAnnotations;

namespace CommentApi.ViewModels.CommentModels
{
    public class CommentModel
    {
        [Required]
        [MaxLength(100)]
        public string Comment {get; set;}
    }
}