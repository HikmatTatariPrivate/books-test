namespace books_test.Server.Model
{
    public class Book
    {
        public int Id { get; set; }     
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int? Price { get; set; }
        public User User { get; set; }  // Navigation property to User
        public string UserUsername { get; set; } // Foreign key to User table
    }
}
