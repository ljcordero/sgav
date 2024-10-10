using System.ComponentModel.DataAnnotations;

namespace SistemaGestionAlquilerVehiculos.Models
{
    public class IngresosPorAlquileresViewModel
    {
        public string Mes { get; set; }

        [DisplayFormat(DataFormatString = "RD$ {0:#,##0.00}")]
        public decimal Precio { get; set; }

        public string? Color { get; set; }
    }
}
