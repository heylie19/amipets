
using Microsoft.EntityFrameworkCore;
using AmiPets.Domain.Entities;

namespace AmiPets.Infrastructure.Data;

// Contexto de conexión con la base de datos
public class AmiPetsDBContext : DbContext
{
    public AmiPetsDBContext(DbContextOptions<AmiPetsDBContext> options)
        : base(options) { }

    // Tabla de usuarios
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    // Configuración de las propiedades de tipo enum
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
