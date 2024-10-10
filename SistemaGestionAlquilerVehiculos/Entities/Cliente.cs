namespace SistemaGestionAlquilerVehiculos.Entities
{
    public class Cliente : BaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Compania { get; set; }

        public ICollection<Alquiler> Alquileres { get; set; }
    }
}
