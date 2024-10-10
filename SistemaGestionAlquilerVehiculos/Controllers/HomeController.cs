using Microsoft.AspNetCore.Mvc;
using SistemaGestionAlquilerVehiculos.Models;
using SistemaGestionAlquilerVehiculos.Repositories;
using System.Diagnostics;

namespace SistemaGestionAlquilerVehiculos.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClienteRepository _clienteRepository;

        public HomeController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
