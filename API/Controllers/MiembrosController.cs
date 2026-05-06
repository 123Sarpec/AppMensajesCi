using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Database;
using API.Entities;
using Microsoft.AspNetCore.Authorization;


namespace API.Controllers;


[ApiController]//clase base para la api
[Route("api/[controller]")]//ruta de la api 


public class MiembrosController(DBContext context) : BaseApiController
{
    [HttpGet] //metodo get
    public async Task<ActionResult<IReadOnlyList<Usuario>>> GetMiembros()
    {
        var Miembros = await context.Usuarios.ToListAsync();
        return Miembros;
    }
    // return context.Usuarios.ToList();
    // }
    [Authorize]

    [HttpGet("{id}")]/* localhost:5103/api/Miembros/ws */
    public async Task<ActionResult<Usuario>> GetMiembro(string id)
    {
        var Miembros = await context.Usuarios.FindAsync(id);
        // return Miembros;
        if (Miembros == null) return NotFound();
        return Miembros;
    }
}
