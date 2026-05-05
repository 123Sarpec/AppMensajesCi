
using API.Entities;
namespace API.Interfaces;


public interface TokenServicio
{
    string CreateToken(Usuario usuario);
}