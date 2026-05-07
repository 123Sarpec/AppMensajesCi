using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using API.Database;
using API.Entities;
using System.Text;
using API.DTOCapas;
using API.Interfaces;
using API.Extension;


namespace API.Controllers;


[ApiController]
[Route("api/[controller]")]//ruta de la api 
public class CrearCuentaController(DBContext context, TokenServicio tokenServicios) : BaseApiController
{
    [HttpPost("registrar")]
    public async Task<ActionResult<UsuariosSesion>> CrearCuenta(RegistratCuenta registrocuenta)
    {
        /*verificar que el correo no exista*/
        if (await VerificarCorreo(registrocuenta.Email)) return BadRequest("El correo ya existe");

        using var hmac = new HMACSHA512();
        var usuario = new Usuario
        {
            Nombre = registrocuenta.Nombre,
            Email = registrocuenta.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registrocuenta.Password)),
            PasswordSalt = hmac.Key
            // };
            //     Email = email,
            //     PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
            //     PasswordSalt = hmac.Key

        };
        /*AGREGAR EL NUEVO USUARIO*/
        context.Usuarios.Add(usuario);
        await context.SaveChangesAsync();

        // return new UsuariosSesion
        // {
        //     Id = usuario.Id,
        //     Email = usuario.Email,
        //     Nomre = usuario.Nombre,
        //     Token = tokenServicios.CreateToken(usuario)
        //     // ImagenUrl = usuario.ImagenUrl
        // };
        return usuario.ToIniciarSesion(tokenServicios);
    }

    /*CREAR EL METOOD PARA INICIAR SESION EL USUAIRO */
    [HttpPost("login")]
    public async Task<ActionResult<UsuariosSesion>> IniciarSesion(IniciarSesion iniciarsesion)
    {
        var usuario = await context.Usuarios
            .SingleOrDefaultAsync(x => x.Email == iniciarsesion.Email);

        if (usuario == null)
            return BadRequest(new { mensaje = "El correo no es valido" });

        using var hmac = new HMACSHA512(usuario.PasswordSalt);

        var passwordHash = hmac.ComputeHash(
            Encoding.UTF8.GetBytes(iniciarsesion.Password));

        for (var i = 0; i < passwordHash.Length; i++)
        {
            if (passwordHash[i] != usuario.PasswordHash[i])
                return BadRequest(new { mensaje = "Contraseña incorrecta" });
        }

        // return new UsuariosSesion
        // {
        //     Id = usuario.Id,
        //     Email = usuario.Email,
        //     Nomre = usuario.Nombre,
        //     Token = tokenServicios.CreateToken(usuario)
        // };
        return usuario.ToIniciarSesion(tokenServicios);
    }
    /*VERIFICAR QUE EL CORREO NO EXISTA*/
    private async Task<bool> VerificarCorreo(string email)
    {
        return await context.Usuarios.AnyAsync(x => x.Email.ToLower() == email.ToLower());
    }
}