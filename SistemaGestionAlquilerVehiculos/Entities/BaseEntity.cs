namespace SistemaGestionAlquilerVehiculos.Entities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
        public virtual bool Borrado { get; set; }
    }
}
