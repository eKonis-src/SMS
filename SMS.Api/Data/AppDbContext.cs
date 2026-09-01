using SMS.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Ems.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<People> Employees => Set<People>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<People>()
            .HasIndex(e => e.Email)
            .IsUnique();
    }
}