using System.ComponentModel.DataAnnotations;

namespace books_test.Server.Model
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<RefreshToken>? RefreshTokens { get; set; }
    }
}
