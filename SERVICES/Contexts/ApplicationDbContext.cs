using Microsoft.EntityFrameworkCore;
using SERVICES.Models;

namespace SERVICES.Contexts
{
    public partial class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration, DbContextOptions<ApplicationDbContext> options)
    : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<DetallesVentum> DetallesVenta { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Venta> Ventas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("SuanyBD");
                if (!string.IsNullOrEmpty(connectionString))
                {
                    optionsBuilder.UseSqlServer(connectionString);
                }
                else
                {
                    //TODO: Log error
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetallesVentum>(entity =>
            {
                entity.HasKey(e => e.DetalleId)
                    .HasName("PK__Detalles__91B12E7015BBE941");

                entity.ToTable("Detalles_Venta");

                entity.Property(e => e.DetalleId)
                    .ValueGeneratedNever()
                    .HasColumnName("detalle_id");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.ProductoId).HasColumnName("producto_id");

                entity.Property(e => e.VentaId).HasColumnName("venta_id");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.DetallesVenta)
                    .HasForeignKey(d => d.ProductoId)
                    .HasConstraintName("FK__Detalles___produ__4E88ABD4");

                entity.HasOne(d => d.Venta)
                    .WithMany(p => p.DetallesVenta)
                    .HasForeignKey(d => d.VentaId)
                    .HasConstraintName("FK__Detalles___venta__4D94879B");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.Property(e => e.ProductoId)
                    .ValueGeneratedNever()
                    .HasColumnName("producto_id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precio");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.Property(e => e.VentaId)
                    .ValueGeneratedNever()
                    .HasColumnName("venta_id");

                entity.Property(e => e.ClienteId).HasColumnName("cliente_id");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("monto");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
