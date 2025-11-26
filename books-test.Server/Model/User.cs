using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace books_test.Server.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<RefreshToken>? RefreshTokens { get; set; }
    }
}
