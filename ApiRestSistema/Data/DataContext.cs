using ApiRestSistema.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRestSistema.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
             : base(options)
        {
        }

        public DbSet<Product> Vehiculos { get; set; }
        public DbSet<Venta> Ventas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la entidad Product
            modelBuilder.Entity<Product>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);

            // Configuración de la entidad Venta
            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(v => v.IdVenta);

                entity.Property(v => v.IdVenta)
                      .HasColumnName("idVenta");

                entity.Property(v => v.NumeroVenta)
                      .HasMaxLength(20)
                      .IsUnicode(false)
                      .HasColumnName("numeroVenta");

                entity.Property(v => v.FechaRegistro)
                      .HasDefaultValueSql("(getdate())")
                      .HasColumnType("datetime")
                      .HasColumnName("fechaRegistro");

                entity.Property(v => v.IdVehiculo)
                      .HasColumnName("idVehiculo");

                entity.Property(v => v.DescripcionVehiculo)
                      .HasMaxLength(200)
                      .IsUnicode(false)
                      .HasColumnName("descripcionVehiculo");

                entity.Property(v => v.PrecioVenta)
                      .HasColumnType("decimal(18, 2)")
                      .HasColumnName("precioVenta");

                entity.Property(v => v.TipoPago)
                      .HasMaxLength(50)
                      .IsUnicode(false)
                      .HasColumnName("tipoPago");

                entity.Property(v => v.Cliente)
                      .HasMaxLength(100)
                      .IsUnicode(false)
                      .HasColumnName("cliente");

                // Relación con Product
                entity.HasOne(v => v.Vehiculo)
                      .WithMany()
                      .HasForeignKey(v => v.IdVehiculo)
                      .HasConstraintName("FK_Venta_Vehiculo");
            });
        }
    }
}