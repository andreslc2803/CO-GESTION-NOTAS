using GestionNotas.Api.Domain.Entities;
using GestionNotas.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionNotas.Api.Infrastructure.Persistence;

public class GestionNotasDbContext : DbContext
{
    public GestionNotasDbContext(DbContextOptions<GestionNotasDbContext> options) : base(options) { }

    //public DbSet<Guia> Guias => Set<Guia>();
    public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
    public DbSet<Profesor> Profesores => Set<Profesor>();
    public DbSet<Nota> Notas => Set<Nota>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Nota>()
            .HasOne(n => n.Profesor)
            .WithMany(p => p.Notas)
            .HasForeignKey(n => n.IdProfesor)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Nota>()
            .HasOne(n => n.Estudiante)
            .WithMany(e => e.Notas)
            .HasForeignKey(n => n.IdEstudiante)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GestionNotasDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
