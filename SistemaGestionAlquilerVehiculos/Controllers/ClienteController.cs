using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionAlquilerVehiculos.Models;
using SistemaGestionAlquilerVehiculos.Repositories;

namespace SistemaGestionAlquilerVehiculos.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        // GET: ClienteController
        public async Task<IActionResult> Index()
        {
            ViewBag.Message = TempData["message"];

            var clientes = await _clienteRepository.FindBy(x => !x.Borrado).ToListAsync();
            return View(clientes.Select(x => new ClienteViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                Cedula = x.Cedula,
                Telefono = x.Telefono,
                Direccion = x.Direccion,
                Compania = x.Compania,
            }).ToList());
        }

        // GET: ClienteController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var cliente = await _clienteRepository.FindBy(x => x.Id == id).FirstAsync();
            return View(new ClienteViewModel
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Cedula = cliente.Cedula,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                Compania = cliente.Compania,
            });
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            var cliente = new Entities.Cliente
            {
                Nombre = collection["Nombre"],
                Apellido = collection["Apellido"],
                Cedula = collection["Cedula"],
                Telefono = collection["Telefono"],
                Direccion = collection["Direccion"],
                Compania = collection["Compania"]
            };

            if (ModelState.IsValid)
            {
                var cedula = collection["Cedula"];

                if (await _clienteRepository.FindBy(x => x.Cedula.Equals(cedula)).AnyAsync())
                {
                    ModelState.AddModelError("Cedula", "Cédula ya registrada");
                    return View(new ClienteViewModel
                    {
                        Id = cliente.Id,
                        Nombre = cliente.Nombre,
                        Apellido = cliente.Apellido,
                        Cedula = cliente.Cedula,
                        Telefono = cliente.Telefono,
                        Direccion = cliente.Direccion,
                        Compania = cliente.Compania,
                    });
                }

                var result = await _clienteRepository.Add(new Entities.Cliente
                {
                    Nombre = collection["Nombre"],
                    Apellido = collection["Apellido"],
                    Cedula = cedula,
                    Telefono = collection["Telefono"],
                    Direccion = collection["Direccion"],
                    Compania = collection["Compania"]
                });

                await _clienteRepository.SaveAsync();

                TempData["message"] = "Cliente Creado";

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: ClienteController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _clienteRepository.FindBy(x => x.Id == id).FirstAsync();
            return View(new ClienteViewModel
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Cedula = cliente.Cedula,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                Compania = cliente.Compania,
            });
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            var cliente = new Entities.Cliente
            {
                Id = id,
                Nombre = collection["Nombre"],
                Apellido = collection["Apellido"],
                Cedula = collection["Cedula"],
                Telefono = collection["Telefono"],
                Direccion = collection["Direccion"],
                Compania = collection["Compania"]
            };

            if (await _clienteRepository.FindBy(x => x.Id != id && x.Cedula.Equals(cliente.Cedula)).AnyAsync())
            {
                ModelState.AddModelError("Cedula", "Cédula ya registrada");
                return View(new ClienteViewModel
                {
                    Id = cliente.Id,
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    Cedula = cliente.Cedula,
                    Telefono = cliente.Telefono,
                    Direccion = cliente.Direccion,
                    Compania = cliente.Compania,
                });
            }

            if (ModelState.IsValid)
            {
                _clienteRepository.Update(cliente);

                await _clienteRepository.SaveAsync();

                TempData["message"] = "Cliente Actualizado";

                return RedirectToAction(nameof(Index));
            }

            return View(new ClienteViewModel
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Cedula = cliente.Cedula,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                Compania = cliente.Compania,
            });
        }

        // GET: ClienteController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _clienteRepository.FindBy(x => x.Id == id).FirstAsync();
            return View(new ClienteViewModel
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Cedula = cliente.Cedula,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                Compania = cliente.Compania,
            });
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            var cliente = await _clienteRepository.FindBy(x => x.Id == id).FirstAsync();

            _clienteRepository.Remove(cliente);

            await _clienteRepository.SaveAsync();

            TempData["message"] = "Cliente Eliminado";

            return RedirectToAction(nameof(Index));
        }
    }
}
