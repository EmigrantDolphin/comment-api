using Microsoft.Extensions.Options;

namespace CommentApi.Options
{
    public class JwtTokenOptions
    {
        public string Secret {get; set;}
        public string Issuer {get; set;}
        public string Audience {get; set;}
        public int AccessTokenExpiration {get; set;}
        public int RefreshTokenExpiration {get; set;}
    }

    public class JwtTokenConfiguration
    {
        private readonly JwtTokenOptions _jwtTokenOptions;
        public JwtTokenConfiguration(IOptions<JwtTokenOptions> options)
        {
            _jwtTokenOptions = options.Value;
        }

        public string Secret => _jwtTokenOptions.Secret;
        public string Issuer => _jwtTokenOptions.Issuer;
        public string Audience => _jwtTokenOptions.Audience;
        public int AccessTokenExpiration => _jwtTokenOptions.AccessTokenExpiration;
        public int RefreshTokenExpiration => _jwtTokenOptions.RefreshTokenExpiration;
    }
}