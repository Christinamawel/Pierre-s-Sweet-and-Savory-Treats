using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Pierres.Models
{
  public class PierresContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Treat> Treats { get; set; }

    public PierresContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}