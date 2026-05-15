using System;
using API.DTOCapas;
using API.Entities;
using API.Interfaces;


namespace API.Extension;

public static class ExtensionUsuario
{
    public static UsuariosSesion ToIniciarSesion(this Usuario usuario, TokenServicio tokenServicios)
    {
        return new UsuariosSesion
        {
            Id = usuario.Id,
            Email = usuario.Email,
            Nombre = usuario.Nombre,
            Token = tokenServicios.CreateToken(usuario)
        };
    }
}