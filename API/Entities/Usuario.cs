using System;
namespace API.Entities;

public class Usuario
{
    /* Usuario Id  usar Guid.NewGuid().ToString(); que defina una guia de cadena de caracteres */
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Nombre { get; set; }
    public required string Email { get; set; }
    /*permite almacenar la contraseña en formato hash */
    public string? ImagenUrl { get; set; }
    public required byte[] PasswordHash { get; set; }
    /*permite almacenar la contraseña en formato hash */
    public required byte[] PasswordSalt { get; set; }

}