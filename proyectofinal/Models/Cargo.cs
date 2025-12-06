using System.ComponentModel.DataAnnotations;

namespace proyectofinal.Models;

public class Cargo
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Nombre del Cargo")]
    public string Nombre { get; set; } = string.Empty;
}

