using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using API.Database;
using API.Entities;


namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]//ruta de la api 
public class MiembrosController(DBContext context) : ControllerBase
{
    [HttpGet] //metodo get
    public async Task<ActionResult<IEnumerable<Usuario>>> GetMiembros()
    {
        var Miembros = await context.Usuarios.ToListAsync();
        return Miembros;
    }
    // return context.Usuarios.ToList();
    // }
    [HttpGet("{id}")]/* localhost:5103/api/Miembros/ws */
    public async Task<ActionResult<Usuario>> GetMiembro(string id)
    {
        var Miembros = await context.Usuarios.FindAsync(id);
        // return Miembros;
        if (Miembros == null) return NotFound();
        return Miembros;
    }
}