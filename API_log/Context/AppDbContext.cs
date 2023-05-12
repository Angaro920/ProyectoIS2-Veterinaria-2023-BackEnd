using API_Log.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Log.Context
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public virtual DbSet<TblAuditoria> TblAuditoria { get; set; }
        public virtual DbSet<TblCliente> TblCliente { get; set; }
        public virtual DbSet<TblClienteHistoriaMascota> TblClienteHistoriaMascota { get; set; }
        public virtual DbSet<TblEmpleados> TblEmpleados { get; set; }
        public virtual DbSet<TblEstados> TblEstados { get; set; }
        public virtual DbSet<TblHistoriaClinica> TblHistoriaClinica { get; set; }
        public virtual DbSet<TblMascotas> TblMascotas { get; set; }
        public virtual DbSet<TblProductos> TblProductos { get; set; }
        public virtual DbSet<TblRaza> TblRaza { get; set; }
        public virtual DbSet<TblTipoMascota> TblTipoMascota { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-QFN78BI\\SQLEXPRESS;Database=Veterinaria;Trusted_Connection=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAuditoria>(entity =>
            {
                entity.HasKey(e => e.IdAccion)
                    .HasName("PK_tbl_Auditoria");

                entity.ToTable("Tbl_Auditoria");

                entity.Property(e => e.IdAccion)
                    .HasColumnName("ID_Accion")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Accion)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.FechaHora)
                    .IsRequired()
                    .HasColumnName("Fecha_Hora")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");
            });

            modelBuilder.Entity<TblCliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.ToTable("Tbl_Cliente");

                entity.Property(e => e.IdCliente)
                    .HasColumnName("ID_Cliente")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.FkIdEstado).HasColumnName("FK_ID_Estado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkIdEstadoNavigation)
                    .WithMany(p => p.TblCliente)
                    .HasForeignKey(d => d.FkIdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tbl_Cliente_Tbl_Estados");
            });
            modelBuilder.Entity<TblClienteHistoriaMascota>(entity =>
            {
                entity.HasKey(e => e.IdClienteHistoriaMascota)
                    .HasName("PK_tbl_Cliente_Historia_Mascota");

                entity.ToTable("Tbl_Cliente_Historia_Mascota");

                entity.Property(e => e.IdClienteHistoriaMascota)
                    .HasColumnName("ID_Cliente_Historia_Mascota")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FkIdCliente).HasColumnName("FK_ID_Cliente");

                entity.Property(e => e.FkIdHistoria).HasColumnName("FK_ID_Historia");

                entity.Property(e => e.FkIdMascota).HasColumnName("FK_ID_Mascota");

                entity.HasOne(d => d.FkIdClienteNavigation)
                    .WithMany(p => p.TblClienteHistoriaMascota)
                    .HasForeignKey(d => d.FkIdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Cliente_Historia_Mascota_Tbl_Cliente");

                entity.HasOne(d => d.FkIdHistoriaNavigation)
                    .WithMany(p => p.TblClienteHistoriaMascota)
                    .HasForeignKey(d => d.FkIdHistoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Cliente_Historia_Mascota_Tbl_Historia_Clinica");

                entity.HasOne(d => d.FkIdMascotaNavigation)
                    .WithMany(p => p.TblClienteHistoriaMascota)
                    .HasForeignKey(d => d.FkIdMascota)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tbl_Cliente_Historia_Mascota_Tbl_Mascotas");
            });

            modelBuilder.Entity<TblEmpleados>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado);

                entity.ToTable("Tbl_Empleados");

                entity.Property(e => e.IdEmpleado)
                    .HasColumnName("ID_Empleado")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.FkIdEstado).HasColumnName("FK_ID_Estado");

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkIdEstadoNavigation)
                    .WithMany(p => p.TblEmpleados)
                    .HasForeignKey(d => d.FkIdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tbl_Empleados_Tbl_Estados");
            });

            modelBuilder.Entity<TblEstados>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.ToTable("Tbl_Estados");

                entity.Property(e => e.IdEstado)
                    .HasColumnName("ID_Estado")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblHistoriaClinica>(entity =>
            {
                entity.HasKey(e => e.IdHistoriaClinica)
                    .HasName("PK_tbl_Historia_Clinica");

                entity.ToTable("Tbl_Historia_Clinica");

                entity.Property(e => e.IdHistoriaClinica)
                    .HasColumnName("ID_Historia_Clinica")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Diagnostico)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Fecha)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FkIdEmpleado).HasColumnName("FK_ID_Empleado");

                entity.Property(e => e.OrdenMedica)
                    .IsRequired()
                    .HasColumnName("Orden_Medica")
                    .HasColumnType("text");

                entity.HasOne(d => d.FkIdEmpleadoNavigation)
                    .WithMany(p => p.TblHistoriaClinica)
                    .HasForeignKey(d => d.FkIdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Historia_Clinica_Tbl_Empleados");
            });

            modelBuilder.Entity<TblMascotas>(entity =>
            {
                entity.HasKey(e => e.IdMascota);

                entity.ToTable("Tbl_Mascotas");

                entity.Property(e => e.IdMascota)
                    .HasColumnName("ID_Mascota")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.FkIdCliente).HasColumnName("FK_ID_Cliente");

                entity.Property(e => e.FkIdEstado).HasColumnName("FK_ID_Estado");

                entity.Property(e => e.FkIdRaza).HasColumnName("FK_ID_Raza");

                entity.Property(e => e.FkIdTipoMascota).HasColumnName("FK_ID_Tipo_Mascota");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkIdClienteNavigation)
                    .WithMany(p => p.TblMascotas)
                    .HasForeignKey(d => d.FkIdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tbl_Mascotas_Tbl_Cliente");

                entity.HasOne(d => d.FkIdEstadoNavigation)
                    .WithMany(p => p.TblMascotas)
                    .HasForeignKey(d => d.FkIdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tbl_Mascotas_Tbl_Estados");

                entity.HasOne(d => d.FkIdRazaNavigation)
                    .WithMany(p => p.TblMascotas)
                    .HasForeignKey(d => d.FkIdRaza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tbl_Mascotas_Tbl_Raza");

                entity.HasOne(d => d.FkIdTipoMascotaNavigation)
                    .WithMany(p => p.TblMascotas)
                    .HasForeignKey(d => d.FkIdTipoMascota)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tbl_Mascotas_Tbl_TipoMascota");
            });

            modelBuilder.Entity<TblProductos>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.ToTable("Tbl_Productos");

                entity.Property(e => e.IdProducto)
                    .HasColumnName("ID_Producto")
                    .ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblRaza>(entity =>
            {
                entity.HasKey(e => e.IdRaza);

                entity.ToTable("Tbl_Raza");

                entity.Property(e => e.IdRaza)
                    .HasColumnName("ID_Raza")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTipoMascota>(entity =>
            {
                entity.HasKey(e => e.IdTipoMascota);

                entity.ToTable("Tbl_TipoMascota");

                entity.Property(e => e.IdTipoMascota)
                    .HasColumnName("ID_Tipo_Mascota")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
