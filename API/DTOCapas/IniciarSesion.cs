using System;

namespace API.DTOCapas;

public class IniciarSesion
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}