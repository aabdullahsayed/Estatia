using Estatia.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Estatia.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Property> Properties { get; set; }



    public DbSet<Division> Divisions { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Area> Areas { get; set; }
}