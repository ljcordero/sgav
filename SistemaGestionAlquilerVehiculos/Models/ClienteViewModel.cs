using System.ComponentModel.DataAnnotations;

namespace SistemaGestionAlquilerVehiculos.Models
{
    public class ClienteViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Entre 2 y 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Entre 2 y 50 caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [RegularExpression("[0-9]{3}-[0-9]{7}-[0-9]{1}", ErrorMessage = "Cédula invalida")]
        [Display(Name = "Cédula", Prompt = "001-0000000-0")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [RegularExpression("\\(8[024]9\\) [0-9]{3}-[0-9]{4}", ErrorMessage = "Teléfono invalido")]
        [Display(Name = "Teléfono", Prompt = "809-000-0000")]
        public string Telefono { get; set; }

        [StringLength(200, MinimumLength = 2, ErrorMessage = "Entre 2 y 200 caracteres")]
        [Display(Name = "Dirección")]
        public string? Direccion { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "Entre 2 y 100 caracteres")]
        [Display(Name = "Compañia")]
        public string? Compania { get; set; }
    }
}
