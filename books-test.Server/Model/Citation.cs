using System.ComponentModel.DataAnnotations.Schema;

namespace books_test.Server.Model
{
    public class Citation
    {
        public int Id { get; set; }
        public string? Text { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public User? User { get; set; }  // Navigation property to User

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string? UserUsername { get; set; } // Foreign key to User table
    }
}
