using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
namespace Test
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { set; get; }

        public DbSet<Blog> Blogs { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL(@"Server=localhost;database=efaaaaaa;uid=root;pwd=Vosung123;SslMode=None");
    }
}
