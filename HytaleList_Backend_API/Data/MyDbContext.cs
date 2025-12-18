using HytaleList_Backend_API.Models;
using Microsoft.EntityFrameworkCore;

namespace HytaleList_Backend_API.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public DbSet<Server> servers { get; set; } = null!;
        public DbSet<User> users { get; set; } = null!;
    }
}



