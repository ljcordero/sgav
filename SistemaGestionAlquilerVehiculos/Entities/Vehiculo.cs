namespace SistemaGestionAlquilerVehiculos.Entities
{
    public class Vehiculo : BaseEntity
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string Matricula { get; set; }
        public string CodigoGps { get; set; }
        public decimal PrecioAlquiler { get; set; }
        public VehiculoEstado Estado { get; set; }

        public ICollection<Alquiler> Alquileres { get; set; }
    }
}
