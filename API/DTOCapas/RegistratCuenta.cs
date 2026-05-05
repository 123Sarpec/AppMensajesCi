using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOCapas;

public class RegistratCuenta
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public required string Nombre { get; set; }

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo que ingreso no es valido")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [MinLength(4, ErrorMessage = "La contraseña debe tener al menos 4 caracteres")]
    public required string Password { get; set; }
}