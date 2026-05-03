using System;
using Microsoft.EntityFrameworkCore;
using API.Entities;

namespace API.Database;

public class DBContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios { get; set; }
}