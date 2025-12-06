using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectofinal.Models
{
    public class Empleado
    {
        [Key]
        [Display(Name = "ID del Empleado")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre del Empleado")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Departamento")]
        public int DepartamentoId { get; set; }

        [Required]
        [Display(Name = "Cargo")]
        public int CargoId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        [Display(Name = "Salario")]
        public decimal Salario { get; set; }

        [Display(Name = "Vigente")]
        public bool Vigente { get; set; }

        public Departamento? Departamento { get; set; }
        public Cargo? Cargo { get; set; }

        // --- CALCULADOS ---

        [NotMapped]
        [Display(Name = "Tiempo en la Empresa")]
        public string TiempoEnEmpresa
        {
            get
            {
                var hoy = DateTime.Today;
                int totalMeses = (hoy.Year - FechaInicio.Year) * 12 + hoy.Month - FechaInicio.Month;
                if (hoy.Day < FechaInicio.Day) totalMeses--;
                int años = totalMeses / 12;
                int meses = totalMeses % 12;
                return $"{años} año(s) y {meses} mes(es)";
            }
        }

        private const decimal AFP_PORC = 0.0287m;
        private const decimal ARS_PORC = 0.0304m;
        private const decimal ISR_PORC = 0.10m;

        [NotMapped]
        public decimal AFP => Math.Round(Salario * AFP_PORC, 2);

        [NotMapped]
        public decimal ARS => Math.Round(Salario * ARS_PORC, 2);

        [NotMapped]
        public decimal ISR => Math.Round(Salario * ISR_PORC, 2);
    }
}
