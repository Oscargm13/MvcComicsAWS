using Microsoft.EntityFrameworkCore;
using MvcComicsAWS.Models;

namespace MvcComicsAWS.Data
{
    public class ComicsContext: DbContext
    {
        public ComicsContext(DbContextOptions<ComicsContext> options) : base(options) { }
        public DbSet<Comic> Comics { get; set; }
    }
}
