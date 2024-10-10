using System.ComponentModel.DataAnnotations;

namespace SistemaGestionAlquilerVehiculos.Entities
{
    public enum VehiculoEstado
    {
        [Display(Name = "Disponible")]
        DISPONIBLE,
        [Display(Name = "Alquilado")]
        ALQUILADO,
        [Display(Name = "No disponible")]
        NO_DISPONIBLE,
    }
}
