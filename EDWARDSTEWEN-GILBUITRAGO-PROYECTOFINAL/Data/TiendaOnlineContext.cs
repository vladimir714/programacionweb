using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EDWARDSTEWEN_GILBUITRAGO_PROYECTOFINAL.Data
{
    public partial class TiendaOnlineContext : DbContext
    {
        public TiendaOnlineContext()
        {
        }

        public TiendaOnlineContext(DbContextOptions<TiendaOnlineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PermisosRol> PermisosRol { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<RolUsuario> RolUsuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TiendaOnline;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PermisosRol>(entity =>
            {
                entity.ToTable("Permisos_Rol");

                entity.Property(e => e.CodigoFuncion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RolId).HasColumnName("Rol_Id");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.PermisosRol)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permisos_Rol_Rol");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RolUsuario>(entity =>
            {
                entity.ToTable("Rol_Usuario");

                entity.Property(e => e.RolId).HasColumnName("Rol_Id");

                entity.Property(e => e.UsuarioId).HasColumnName("Usuario_Id");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.RolUsuario)
                    .HasForeignKey(d => d.RolId)
                    .HasConstraintName("FK_Rol_Usuario_Rol");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.RolUsuario)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_Rol_Usuario_Usuario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Identificacion)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Usuario1)
                    .IsRequired()
                    .HasColumnName("Usuario")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
