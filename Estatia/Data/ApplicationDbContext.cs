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
    public DbSet<Agent> Agents { get; set; }
    public DbSet<User> Users { get; set; }
    
}

