using SistemaGestionAlquilerVehiculos.Entities;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestionAlquilerVehiculos.Models
{
    public class AlquilarVehiculoViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Vehículo")]

        public Vehiculo Vehiculo { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Cliente")]
        public Cliente Cliente { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Fecha Inicio")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Fecha Fin")]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }

        public bool Devuelto { get; set; }

        [Display(Name = "Saldo")]
        [DisplayFormat(DataFormatString = "RD$ {0:#,##0.00}")]
        public decimal Saldo { get; set; }
    }
}
