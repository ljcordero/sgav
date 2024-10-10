namespace SistemaGestionAlquilerVehiculos.Entities
{
    public class Alquiler : BaseEntity
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Devuelto { get; set; }
        public decimal PrecioAlquilerVehiculo { get; set; }
        public decimal PrecioTotal { get; set; }

        public int VehiculoId { get; set; }
        public int ClienteId { get; set; }

        public Vehiculo Vehiculo { get; set; }
        public Cliente Cliente { get; set; }
    }
}
