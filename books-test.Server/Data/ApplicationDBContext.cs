using Microsoft.EntityFrameworkCore;
using books_test.Server.Model;

namespace books_test.Server.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Citation> Citations { get; set; }
    }
}
