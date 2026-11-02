using Microsoft.EntityFrameworkCore;
using AmiPets.Domain.Entities;

namespace AmiPets.Infrastructure.Data;

public class AmiPetsDBContext : DbContext
{
    public AmiPetsDBContext(DbContextOptions<AmiPetsDBContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .Property(u => u.Rol)
            .HasConversion<string>()
            .HasMaxLength(20);

        modelBuilder.Entity<Usuario>()
            .Property(u => u.Estado)
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}