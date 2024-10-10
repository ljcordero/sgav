using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionAlquilerVehiculos.Entities;
using SistemaGestionAlquilerVehiculos.Models;
using SistemaGestionAlquilerVehiculos.Repositories;

namespace SistemaGestionAlquilerVehiculos.Controllers
{
    public class VehiculoController : Controller
    {
        private readonly IVehiculoRepository _vehiculoRepository;

        public VehiculoController(IVehiculoRepository vehiculoRepository)
        {
            _vehiculoRepository = vehiculoRepository;
        }

        // GET: VehiculoController
        public async Task<IActionResult> Index()
        {
            ViewBag.Message = TempData["message"];

            var vehiculos = await _vehiculoRepository.FindBy(x => !x.Borrado).ToListAsync();
            return View(vehiculos.Select(x => new VehiculoViewModel
            {
                Id = x.Id,
                Marca = x.Marca,
                Modelo = x.Modelo,
                Year = x.Year,
                Color = x.Color,
                Matricula = x.Matricula,
                CodigoGps = x.CodigoGps,
                PrecioAlquiler = x.PrecioAlquiler,
                Estado = x.Estado,
            }).ToList());
        }

        // GET: VehiculoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var vehiculo = await _vehiculoRepository.FindBy(x => x.Id == id).FirstAsync();
            return View(new VehiculoViewModel
            {
                Id = vehiculo.Id,
                Marca = vehiculo.Marca,
                Modelo = vehiculo.Modelo,
                Year = vehiculo.Year,
                Color = vehiculo.Color,
                Matricula = vehiculo.Matricula,
                CodigoGps = vehiculo.CodigoGps,
                PrecioAlquiler = vehiculo.PrecioAlquiler,
                Estado = vehiculo.Estado,
            });
        }

        // GET: VehiculoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehiculoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            var a = collection["Estado"];
            if (ModelState.IsValid)
            {
                var result = await _vehiculoRepository.Add(new Vehiculo
                {
                    Marca = collection["Marca"],
                    Modelo = collection["Modelo"],
                    Year = Convert.ToInt32(collection["Year"]),
                    Color = collection["Color"],
                    Matricula = collection["Matricula"],
                    CodigoGps = collection["CodigoGps"],
                    PrecioAlquiler = Convert.ToDecimal(collection["PrecioAlquiler"]),
                    Estado = (VehiculoEstado) Enum.Parse(typeof(VehiculoEstado), collection["Estado"])
                });

                await _vehiculoRepository.SaveAsync();

                TempData["message"] = "Vehículo Creado";

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: VehiculoController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var vehiculo = await _vehiculoRepository.FindBy(x => x.Id == id).FirstAsync();
            return View(new VehiculoViewModel
            {
                Id = vehiculo.Id,
                Marca = vehiculo.Marca,
                Modelo = vehiculo.Modelo,
                Year = vehiculo.Year,
                Color = vehiculo.Color,
                Matricula = vehiculo.Matricula,
                CodigoGps = vehiculo.CodigoGps,
                PrecioAlquiler = vehiculo.PrecioAlquiler,
                Estado = vehiculo.Estado,
            });
        }

        // POST: VehiculoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            var vehiculo = new Vehiculo
            {
                Id = id,
                Marca = collection["Marca"],
                Modelo = collection["Modelo"],
                Year = Convert.ToInt32(collection["Year"]),
                Color = collection["Color"],
                Matricula = collection["Matricula"],
                CodigoGps = collection["CodigoGps"],
                PrecioAlquiler = Convert.ToDecimal(collection["PrecioAlquiler"]),
                Estado = (VehiculoEstado)Enum.Parse(typeof(VehiculoEstado), collection["Estado"])
            };

            if (ModelState.IsValid)
            {
                _vehiculoRepository.Update(vehiculo);

                await _vehiculoRepository.SaveAsync();

                TempData["message"] = "Vehículo Actualizado";

                return RedirectToAction(nameof(Index));
            }

            return View(new VehiculoViewModel
            {
                Id = vehiculo.Id,
                Marca = vehiculo.Marca,
                Modelo = vehiculo.Modelo,
                Year = vehiculo.Year,
                Color = vehiculo.Color,
                Matricula = vehiculo.Matricula,
                CodigoGps = vehiculo.CodigoGps,
                PrecioAlquiler = vehiculo.PrecioAlquiler,
                Estado = vehiculo.Estado,
            });
        }

        // GET: VehiculoController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var vehiculo = await _vehiculoRepository.FindBy(x => x.Id == id).FirstAsync();
            return View(new VehiculoViewModel
            {
                Id = vehiculo.Id,
                Marca = vehiculo.Marca,
                Modelo = vehiculo.Modelo,
                Year = vehiculo.Year,
                Color = vehiculo.Color,
                Matricula = vehiculo.Matricula,
                CodigoGps = vehiculo.CodigoGps,
                PrecioAlquiler = vehiculo.PrecioAlquiler,
                Estado = vehiculo.Estado,
            });
        }

        // POST: VehiculoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            var vehiculo = await _vehiculoRepository.FindBy(x => x.Id == id).FirstAsync();

            _vehiculoRepository.Remove(vehiculo);

            await _vehiculoRepository.SaveAsync();

            TempData["message"] = "Vehículo Eliminado";

            return RedirectToAction(nameof(Index));
        }
    }
}
