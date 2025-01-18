using ApiRestSistema.Data;
using ApiRestSistema.Interfaces;
using ApiRestSistema.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ApiRestSistema.Services
{
    public class VentaRepositorio : IVentaRepositorio
    {
        private readonly DataContext _context;

        public VentaRepositorio(DataContext context)
        {
            _context = context;
        }

        public async Task<Venta> Registrar(Venta venta)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var ultimoNumeroVenta = await _context.Ventas
                    .OrderByDescending(v => v.IdVenta)
                    .Select(v => v.NumeroVenta)
                    .FirstOrDefaultAsync();

                int nuevoNumeroVenta = string.IsNullOrEmpty(ultimoNumeroVenta) ? 1 : int.Parse(ultimoNumeroVenta) + 1;
                venta.NumeroVenta = nuevoNumeroVenta.ToString("D4");
                venta.FechaRegistro = DateTime.Now;

                await _context.Ventas.AddAsync(venta);
                await _context.SaveChangesAsync();

                var vehiculo = await _context.Vehiculos.FindAsync(venta.IdVehiculo);
                if (vehiculo == null)
                    throw new ArgumentException($"El vehículo con ID {venta.IdVehiculo} no existe.");

                if (vehiculo.Estado == "Reservado")
                    throw new InvalidOperationException($"El vehículo {vehiculo.Marca} {vehiculo.Modelo} ya está reservado.");

                vehiculo.Estado = "Reservado";
                _context.Vehiculos.Update(vehiculo);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return venta;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<Venta>> Historial(string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Venta> query = _context.Ventas.Include(v => v.Vehiculo);

            if (!string.IsNullOrWhiteSpace(numeroVenta))
            {
                query = query.Where(v => v.NumeroVenta == numeroVenta);
            }

            if (DateTime.TryParse(fechaInicio, out var fechaInicioParsed))
            {
                query = query.Where(v => v.FechaRegistro >= fechaInicioParsed);
            }

            if (DateTime.TryParse(fechaFin, out var fechaFinParsed))
            {
                query = query.Where(v => v.FechaRegistro <= fechaFinParsed);
            }

            return await query.ToListAsync();
        }

        
    }
}