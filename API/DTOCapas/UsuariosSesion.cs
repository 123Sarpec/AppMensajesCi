using System;

namespace API.DTOCapas;

public class UsuariosSesion
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public required string Nombre { get; set; }
    public string? ImagenUrl { get; set; }
    public required string Token { get; set; }
}