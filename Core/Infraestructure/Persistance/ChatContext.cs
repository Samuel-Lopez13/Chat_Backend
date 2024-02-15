using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Infraestructure.Persistance;

public class ChatContext : DbContext
{
    public ChatContext(DbContextOptions<ChatContext> options): base(options){ }

    public DbSet<Grupo> Grupos { get; set; } = null!;
    public DbSet<Mensaje> Mensajes { get; set; } = null!;
    public DbSet<Usuario> Usuarios { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.Id_Grupo);
        });
        
        modelBuilder.Entity<Mensaje>(entity =>
        {
            entity.HasKey(e => e.Id_Mensaje);
            
            entity.HasOne(d => d.Grupos)
                .WithMany(p => p.Mensajes)
                .HasForeignKey(d => d.Id_Mensaje)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("grupos_ibfk_1");
            
            entity.HasOne(d => d.Usuarios)
                .WithMany(p => p.Mensajes)
                .HasForeignKey(d => d.Id_Usuario)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("usuarios_ibfk_1");
        });
        
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id_Usuario);

            entity.Property(e => e.Contrasena).IsRequired();
            
            entity.Property(e => e.UserName).IsRequired();
            entity.HasIndex(e => e.UserName).IsUnique();
        });
        
        base.OnModelCreating(modelBuilder);
    }
}