using ApiRestSistema.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRestSistema.Data
{

    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options)
             : base(options)
        {
        }

        public DbSet<Product> Vehiculos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<NumeroDocumento> NumeroDocumentos { get; set; }
        public DbSet<DetalleVenta> DetalleVenta { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura la precisión y escala usando HasPrecision
            modelBuilder.Entity<Product>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.HasKey(e => e.IdDetalleVenta).HasName("PK__DetalleV__BFE2843F4E8F137D");

                entity.Property(e => e.IdDetalleVenta).HasColumnName("idDetalleVenta");
                entity.Property(e => e.IdProducto).HasColumnName("idProducto");
                entity.Property(e => e.IdVenta).HasColumnName("idVenta");
                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precio");
                entity.Property(e => e.Total)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total");

                entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__DetalleVe__idPro__239E4DCF");

                entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdVenta)
                    .HasConstraintName("FK__DetalleVe__idVen__22AA2996");
            });
            modelBuilder.Entity<NumeroDocumento>(entity =>
            {
                entity.HasKey(e => e.IdNumeroDocumento).HasName("PK__NumeroDo__471E421A2AE15B63");

                entity.ToTable("NumeroDocumento");

                entity.Property(e => e.IdNumeroDocumento).HasColumnName("idNumeroDocumento");
                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");
                entity.Property(e => e.UltimoNumero).HasColumnName("ultimo_Numero");
            });
            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.IdVenta).HasName("PK__Venta__077D5614B7613BCA");

                entity.Property(e => e.IdVenta).HasColumnName("idVenta");
                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");
                entity.Property(e => e.NumeroDocumento)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("numeroDocumento");
                entity.Property(e => e.TipoPago)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipoPago");
                entity.Property(e => e.Total)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total");
            });

        }
    }

}


