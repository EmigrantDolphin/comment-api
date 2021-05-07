
namespace CommentApi.ViewModels
{
    public class ErrorModel 
    {
        public string Error {get; set;}

        public ErrorModel(string message)
        {
            Error = message;
        }
    }
}