using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ConcertApi.Models
{
    public class ConcertContext : DbContext
    {

        public DbSet<Concert> Concerts { get; set; }
        public ConcertContext(DbContextOptions options) : base(options) { }
        
    }
}