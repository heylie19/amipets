using Microsoft.EntityFrameworkCore;
using AmiPets.Domain.Entities;

namespace AmiPets.Infrastructure.Data;

public class AmiPetsDBContext : DbContext
{
    public AmiPetsDBContext(DbContextOptions<AmiPetsDBContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
}