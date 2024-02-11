using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace WebApplication1.Entity
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        
    }
   
}
