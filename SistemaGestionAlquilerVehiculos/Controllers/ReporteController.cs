using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionAlquilerVehiculos.Models;
using SistemaGestionAlquilerVehiculos.Repositories;
using System.Globalization;

namespace SistemaGestionAlquilerVehiculos.Controllers
{
    public class ReporteController : Controller
    {
        private readonly IAlquilerRepository _alquilerRepository;

        public ReporteController(IAlquilerRepository alquilerRepository)
        {
            _alquilerRepository = alquilerRepository;
        }

        // GET: ReporteController/VehiculosAlquilados
        public async Task<IActionResult> VehiculosAlquilados()
        {
            var alquileres = await _alquilerRepository
                .GetAll(x => x.Vehiculo, x => x.Cliente)
                .Where(x => !x.Devuelto && !x.Vehiculo.Borrado && !x.Cliente.Borrado)
                .ToListAsync();

            return View(alquileres.Select(x => new AlquilarVehiculoViewModel
            {
                Id = x.Id,
                Vehiculo = x.Vehiculo,
                Cliente = x.Cliente,
                FechaInicio = x.FechaInicio,
                FechaFin = x.FechaFin,
                Devuelto = x.Devuelto,
                Saldo = x.PrecioTotal,
            }).ToList());
        }

        // GET: ReporteController/IngresosPorAlquileres
        public async Task<IActionResult> IngresosPorAlquileres()
        {
            var alquileres = await _alquilerRepository
                .FindBy(x => x.Devuelto == true && x.FechaFin.Date >= new DateTime(DateTime.Now.Year, 1, 1).Date)
                .ToListAsync();

            var meses = alquileres.OrderByDescending(x => x.FechaFin.Date).GroupBy(
                x => x.FechaFin.ToUniversalTime().ToString("MMMM", new CultureInfo("es-ES")).Pascalize(),
                x => x.PrecioTotal,
                (FechaFin, PrecioTotal) => new
                {
                    Mes = FechaFin,
                    Precio = PrecioTotal.Sum(),
                }
            ).ToList();

            return View(meses.Select((x, i) => new IngresosPorAlquileresViewModel
            {
                Mes = x.Mes,
                Precio = x.Precio,
                Color = meses.Count > (i + 1) ? (x.Precio >= meses[i + 1].Precio ? "#d1e7dd" : "#f8d7da") : ""
            }).ToList());
        }

        // GET: ReporteController/ClientesFrecuentes
        public async Task<IActionResult> ClientesFrecuentes()
        {
            var alquileres = await _alquilerRepository
                .GetAll(x => x.Vehiculo, x => x.Cliente)
                .ToListAsync();
            var clientes = alquileres.OrderByDescending(x => x.FechaFin).GroupBy(
                x => new { Nombre = $"{x.Cliente.Nombre} {x.Cliente.Apellido}", Borrado = x.Cliente.Borrado },
                x => x.Vehiculo,
                (Cliente, Vehiculos) => new
                {
                    Cliente,
                    Vehiculos = Vehiculos.ToList(),
                }
            ).ToList();

            return View(clientes.Select(x => new ClientesFrecuentesViewModel
            {
                Cliente = x.Cliente.Nombre,
                ClienteBorrado = x.Cliente.Borrado,
                Vehiculos = x.Vehiculos,
            }).ToList());
        }
    }
}
