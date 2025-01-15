using ApiRestSistema.Data;
using ApiRestSistema.Interfaces;
using ApiRestSistema.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ApiRestSistema.Services
{
    public class VentaRepositorio : IVentaRepositorio
    {
        private readonly IDbContextFactory<DataContext> _dbContextFactory;

        public VentaRepositorio(IDbContextFactory<DataContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Venta> Registrar(Venta entidad)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Verificar si ya existe un registro en NumeroDocumento
                        var correlativo = await context.NumeroDocumentos.FirstOrDefaultAsync();
                        if (correlativo == null)
                        {
                            // Si no existe, crear un nuevo registro
                            correlativo = new NumeroDocumento
                            {
                                UltimoNumero = 1, // O el número inicial que desees
                                FechaRegistro = DateTime.Now
                            };
                            await context.NumeroDocumentos.AddAsync(correlativo);
                            await context.SaveChangesAsync();
                        }
                        else
                        {
                            // Si existe, incrementar el número
                            correlativo.UltimoNumero++;
                            correlativo.FechaRegistro = DateTime.Now;
                            context.NumeroDocumentos.Update(correlativo);
                            await context.SaveChangesAsync();
                        }

                        // Generar el número de venta
                        string numeroVenta = correlativo.UltimoNumero.ToString("D4");
                        entidad.NumeroDocumento = numeroVenta;

                        // Guardar la venta
                        await context.Ventas.AddAsync(entidad);
                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return entidad;
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }

        public async Task<List<Venta>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                IQueryable<Venta> query = context.Ventas;

                // Validación de parámetros
                if (buscarPor == "NumeroDocumento" && string.IsNullOrWhiteSpace(numeroVenta))
                {
                    throw new ArgumentException("El número de venta es obligatorio cuando se busca por número de documento.");
                }

                if (buscarPor == "fecha")
                {
                    // Parseo de fechas con manejo de errores
                    DateTime fech_Inicio;
                    DateTime fech_Fin;

                    bool inicioValido = DateTime.TryParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"), DateTimeStyles.None, out fech_Inicio);
                    bool finValido = DateTime.TryParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"), DateTimeStyles.None, out fech_Fin);

                    if (!inicioValido || !finValido)
                    {
                        throw new ArgumentException("Las fechas proporcionadas no tienen un formato válido. Asegúrese de usar el formato dd/MM/yyyy.");
                    }

                    // Filtrar por fecha
                    query = query.Where(v => v.FechaRegistro.HasValue &&
                                             v.FechaRegistro.Value >= fech_Inicio.Date &&
                                             v.FechaRegistro.Value <= fech_Fin.Date);
                }
                else if (buscarPor == "NumeroDocumento" && !string.IsNullOrWhiteSpace(numeroVenta))
                {
                    // Filtrar por número de documento
                    query = query.Where(v => v.NumeroDocumento == numeroVenta);
                }
                else
                {
                    throw new ArgumentException("Parámetros de búsqueda no válidos. Asegúrese de que 'buscarPor' sea 'NumeroDocumento' o 'fecha'.");
                }

                // Incluir detalles de la venta y productos relacionados
                return await query.Include(dv => dv.DetalleVenta)
                                  .ThenInclude(p => p.IdProductoNavigation)
                                  .ToListAsync();
            }
        }

        public async Task<List<DetalleVenta>> Reporte(string FechaInicio, string FechaFin)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                DateTime fech_Inicio = DateTime.ParseExact(FechaInicio, "dd/MM/yyyy", new CultureInfo("es-PE"));
                DateTime fech_Fin = DateTime.ParseExact(FechaFin, "dd/MM/yyyy", new CultureInfo("es-PE"));

                return await context.DetalleVenta
                    .Include(p => p.IdProductoNavigation)
                    .Include(v => v.IdVentaNavigation)
                    .Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date >= fech_Inicio.Date && dv.IdVentaNavigation.FechaRegistro.Value.Date <= fech_Fin.Date)
                    .ToListAsync();
            }
        }

        public async Task<DetalleVenta> RegistrarDetalle(DetalleVenta detalle)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var venta = await context.Ventas.FindAsync(detalle.IdVenta);
                if (venta == null)
                {
                    throw new ArgumentException("La venta especificada no existe.");
                }

                // Actualizar el estado del producto
                var producto = await context.Vehiculos.FindAsync(detalle.IdProducto);
                if (producto == null)
                {
                    throw new ArgumentException("El producto especificado no existe.");
                }

                producto.Estado = "Reservado"; // O "Vendido"
                context.Vehiculos.Update(producto);

                // Agregar el detalle de la venta
                await context.DetalleVenta.AddAsync(detalle);
                await context.SaveChangesAsync();

                return detalle;
            }
        }


    }

}
