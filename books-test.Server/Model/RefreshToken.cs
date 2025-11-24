namespace books_test.Server.Model
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string UserUsername { get; set; } // Foreign key to User table
        public string TokenId { get; set; }
        public string RefreshUserToken { get; set; }
        public User User { get; set; }  // Navigation property to User
    }

}