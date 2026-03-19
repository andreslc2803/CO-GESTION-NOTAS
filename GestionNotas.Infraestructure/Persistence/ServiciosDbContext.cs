using Microsoft.EntityFrameworkCore;
using GestionNotas.Api.Domain.Entities;

namespace GestionNotas.Api.Infrastructure.Persistence;

public class ServiciosDbContext : DbContext
{
    public ServiciosDbContext(DbContextOptions<ServiciosDbContext> options) : base(options) { }

    //public DbSet<Guia> Guias => Set<Guia>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServiciosDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
