using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;


namespace API.Servicios;

public class TokenServicios(IConfiguration config) : TokenServicio
{
    public string CreateToken(Usuario usuario)
    {
        // throw new NotImplementedException();
        var tokenKey = config["TokenKey"] ?? throw new Exception("TokenKey no encontrado");
        if (tokenKey.Length < 64) throw new Exception("TokenKey debe tener al menos 64 caracteres");

        /*IDENTIFICAR UNA SEGURIDAD PARA GENERAR EL TOKET*/
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        /*definir como una reclamancion el email del usuario*/
        var claims = new List<Claim>
        {
            new (ClaimTypes.Email, usuario.Email),
            // new Claim(ClaimTypes.Name, usuario.Id),
            new (ClaimTypes.NameIdentifier, usuario.Id)
        };
        /*DEFINIRA LAS CREDNECIALES PARA UTILIZAR EL ALGORITMO DE SEGURIDAD*/
        var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        /*se crea el descriptro del token*/
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims), // identificar al usuario
            Expires = DateTime.UtcNow.AddDays(7),// fecha de expiracion
            SigningCredentials = creds // la clave de seguridad
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

