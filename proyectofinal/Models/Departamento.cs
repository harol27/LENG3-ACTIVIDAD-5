using System.ComponentModel.DataAnnotations;

namespace proyectofinal.Models;

public class Departamento
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Nombre del Departamento")]
    public string Nombre { get; set; } = string.Empty;
}
   