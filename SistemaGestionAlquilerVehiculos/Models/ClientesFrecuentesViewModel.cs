using SistemaGestionAlquilerVehiculos.Entities;

namespace SistemaGestionAlquilerVehiculos.Models
{
    public class ClientesFrecuentesViewModel
    {
        public string Cliente { get; set; }
        public bool ClienteBorrado { get; set; }
        public List<Vehiculo> Vehiculos { get; set; }
    }
}
