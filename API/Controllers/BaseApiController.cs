using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using API.Database;
using API.Entities;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]//ruta de la api 
public class BaseApiController : ControllerBase
{
    // protected readonly DBContext context = context;
}