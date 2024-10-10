using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionAlquilerVehiculos.Entities;
using SistemaGestionAlquilerVehiculos.Models;
using SistemaGestionAlquilerVehiculos.Repositories;

namespace SistemaGestionAlquilerVehiculos.Controllers
{
    public class AlquilerController : Controller
    {
        private readonly IVehiculoRepository _vehiculoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IAlquilerRepository _alquilerRepository;

        public AlquilerController(IVehiculoRepository vehiculoRepository, IClienteRepository clienteRepository, IAlquilerRepository alquilerRepository)
        {
            _vehiculoRepository = vehiculoRepository;
            _clienteRepository = clienteRepository;
            _alquilerRepository = alquilerRepository;
        }

        // GET: AlquilerController
        public async Task<IActionResult> Index()
        {
            ViewBag.Message = TempData["message"];

            var alquileres = await _alquilerRepository.GetAll(x => x.Vehiculo, x => x.Cliente).OrderBy(x => x.Devuelto).ToListAsync();
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

        // GET: AlquilerController/Alquilar
        public async Task<ActionResult> Alquilar()
        {
            ViewBag.Message = TempData["message"];

            ViewBag.Vehiculos = await _vehiculoRepository.FindBy(x => !x.Borrado && x.Estado == VehiculoEstado.DISPONIBLE).ToListAsync();
            ViewBag.Clientes = await _clienteRepository.FindBy(x => !x.Borrado).ToListAsync();

            return View();
        }

        // POST: AlquilerController/Alquilar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Alquilar(IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var vehiculoId = Convert.ToInt32(collection["Vehiculo"]);
                var clienteId = Convert.ToInt32(collection["Cliente"]);
                var fechaInicio = Convert.ToDateTime(collection["FechaInicio"]);
                var fechaFin = Convert.ToDateTime(collection["FechaFin"]);

                var vehiculo = await _vehiculoRepository.FindBy(x => x.Id == vehiculoId).FirstOrDefaultAsync();

                if (vehiculo != null)
                {
                    await _alquilerRepository.Add(new Alquiler
                    {
                        VehiculoId = vehiculoId,
                        ClienteId = clienteId,
                        FechaInicio = fechaInicio,
                        FechaFin = fechaFin,
                        PrecioAlquilerVehiculo = vehiculo.PrecioAlquiler,
                        PrecioTotal = vehiculo.PrecioAlquiler * ((decimal)(fechaFin - fechaInicio).TotalDays + 1),
                    });

                    vehiculo.Estado = VehiculoEstado.ALQUILADO;
                    _vehiculoRepository.Update(vehiculo);

                    await _alquilerRepository.SaveAsync();

                    TempData["message"] = "Vehículo Alquilado";

                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }

        // GET: AlquilerController/Devolver/id
        public async Task<ActionResult> Devolver(int id)
        {
            ViewBag.Message = TempData["message"];

            ViewBag.Alquileres = await _alquilerRepository.FindBy(x => x.Id == id).Include(x => x.Vehiculo).ToListAsync();

            return View();
        }

        // POST: AlquilerController/Devolver/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Devolver(int id, IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var vehiculoId = Convert.ToInt32(collection["Vehiculo"]);

                var vehiculo = await _vehiculoRepository.FindBy(x => x.Id == vehiculoId).FirstOrDefaultAsync();
                var alquiler = await _alquilerRepository.FindBy(x => x.VehiculoId == vehiculoId && x.Devuelto == false).OrderByDescending(x => x.Id).FirstOrDefaultAsync();

                if (vehiculo != null && alquiler != null)
                {
                    alquiler.Devuelto = true;
                    alquiler.FechaInicio = alquiler.FechaInicio.ToUniversalTime();
                    alquiler.FechaFin = alquiler.FechaFin.ToUniversalTime();
                    _alquilerRepository.Update(alquiler);

                    vehiculo.Estado = VehiculoEstado.DISPONIBLE;
                    _vehiculoRepository.Update(vehiculo);

                    await _alquilerRepository.SaveAsync();

                    TempData["message"] = "Vehículo Devuelto";

                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }

        // GET: AlquilerController/Extender/id
        public async Task<ActionResult> Extender(int id)
        {
            ViewBag.Message = TempData["message"];

            ViewBag.Alquileres = await _alquilerRepository.FindBy(x => x.Id == id).Include(x => x.Vehiculo).ToListAsync();

            return View();
        }

        // POST: AlquilerController/Extender/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Extender(int id, IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var vehiculoId = Convert.ToInt32(collection["Vehiculo"]);
                var fechaFin = Convert.ToDateTime(collection["FechaFin"]);

                var alquiler = await _alquilerRepository.FindBy(x => x.VehiculoId == vehiculoId && x.Devuelto == false).OrderByDescending(x => x.Id).FirstOrDefaultAsync();

                if (alquiler != null)
                {
                    alquiler.FechaInicio = alquiler.FechaInicio.ToUniversalTime();
                    alquiler.FechaFin = fechaFin;
                    alquiler.PrecioTotal = alquiler.PrecioAlquilerVehiculo * ((decimal)(fechaFin - alquiler.FechaInicio).TotalDays + 1);
                    _alquilerRepository.Update(alquiler);

                    await _alquilerRepository.SaveAsync();

                    TempData["message"] = "Fecha de entrega extendida";

                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }
    }
}
