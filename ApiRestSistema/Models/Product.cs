namespace ApiRestSistema.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string TipoVehiculo { get; set; }
        public required string Marca { get; set; }
        public required string Modelo { get; set; }
        public required int Anio { get; set; }
        public required string Color { get; set; }
        public required double Kilometraje { get; set; }
        public required decimal Precio { get; set; }
        public required DateTime FechaIngreso { get; set; }
        public required string Estado { get; set; }
        public virtual ICollection<DetalleVenta> DetalleVenta { get; } = new List<DetalleVenta>();

    }
}
