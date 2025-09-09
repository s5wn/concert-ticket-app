using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ConcertApi.Models
{
    public class UserContext : DbContext
    {
        public DbSet<Concert> Concerts { get; set; }
        public UserContext(DbContextOptions options) : base(options) { }
    }
}