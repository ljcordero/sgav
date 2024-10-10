using SistemaGestionAlquilerVehiculos.Entities;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestionAlquilerVehiculos.Models
{
    public class VehiculoViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Año")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Entre 2 y 20 caracteres")]
        [Display(Name = "Matrícula")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Entre 2 y 20 caracteres")]
        [Display(Name = "Código GPS")]
        public string CodigoGps { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [Display(Name = "Precio Alquiler por Día")]
        [RegularExpression("^(0|[1-9]\\d*)(\\.\\d+)?$", ErrorMessage = "Precio invalido")]
        [DisplayFormat(DataFormatString = "RD$ {0:#,##0.00}")]
        public decimal PrecioAlquiler { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public VehiculoEstado Estado { get; set; }
    }
}
