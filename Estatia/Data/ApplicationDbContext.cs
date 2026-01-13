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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);



        modelBuilder.Entity<Division>().HasData(
            new Division { Id = 1, Name = "Dhaka" },
            new Division { Id = 2, Name = "Chattogram" },
            new Division { Id = 3, Name = "Sylhet" },
            new Division { Id = 4, Name = "Khulna" },
            new Division { Id = 5, Name = "Rajshahi" },
            new Division { Id = 6, Name = "Barishal" },
            new Division { Id = 7, Name = "Rangpur" },
            new Division { Id = 8, Name = "Mymensingh" }
        );


        modelBuilder.Entity<City>().HasData(
            
            new City { Id = 1, Name = "Dhaka", DivisionId = 1 },
            new City { Id = 2, Name = "Gazipur", DivisionId = 1 },
            new City { Id = 3, Name = "Narayanganj", DivisionId = 1 },
            new City { Id = 4, Name = "Savar", DivisionId = 1 },

            new City { Id = 5, Name = "Chattogram", DivisionId = 2 },
            new City { Id = 6, Name = "Cox's Bazar", DivisionId = 2 },
            new City { Id = 7, Name = "Cumilla", DivisionId = 2 },

            
            new City { Id = 8, Name = "Sylhet", DivisionId = 3 },
            new City { Id = 9, Name = "Khulna", DivisionId = 4 },
            new City { Id = 10, Name = "Rajshahi", DivisionId = 5 },
            new City { Id = 11, Name = "Barishal", DivisionId = 6 },
            new City { Id = 12, Name = "Rangpur", DivisionId = 7 },
            new City { Id = 13, Name = "Mymensingh", DivisionId = 8 }
        );

     
        modelBuilder.Entity<Area>().HasData(
            
            new Area { Id = 1, Name = "Gulshan", CityId = 1 },
            new Area { Id = 2, Name = "Banani", CityId = 1 },
            new Area { Id = 3, Name = "Baridhara", CityId = 1 },
            new Area { Id = 4, Name = "Dhanmondi", CityId = 1 },
            new Area { Id = 5, Name = "Mirpur", CityId = 1 },
            new Area { Id = 6, Name = "Uttara", CityId = 1 },
            new Area { Id = 7, Name = "Mohammadpur", CityId = 1 },
            new Area { Id = 8, Name = "Bashundhara", CityId = 1 },

            
            new Area { Id = 9, Name = "Tongi", CityId = 2 },
            new Area { Id = 10, Name = "Board Bazar", CityId = 2 },

            
            new Area { Id = 11, Name = "Chashara", CityId = 3 },
            new Area { Id = 12, Name = "Fatullah", CityId = 3 },

          
            new Area { Id = 13, Name = "Ashulia", CityId = 4 },

            
            new Area { Id = 14, Name = "Agrabad", CityId = 5 },
            new Area { Id = 15, Name = "GEC Circle", CityId = 5 },
            new Area { Id = 16, Name = "Nasirabad", CityId = 5 },
            new Area { Id = 17, Name = "Pahartali", CityId = 5 },
            new Area { Id = 18, Name = "Halishahar", CityId = 5 },

            new Area { Id = 19, Name = "Kolatoli", CityId = 6 },
            new Area { Id = 20, Name = "Sugandha", CityId = 6 },

           
            new Area { Id = 21, Name = "Kandirpar", CityId = 7 },

            
            new Area { Id = 22, Name = "Zindabazar", CityId = 8 },
            new Area { Id = 23, Name = "Sonadanga", CityId = 9 },
            new Area { Id = 24, Name = "Shaheb Bazar", CityId = 10 },
            new Area { Id = 25, Name = "Nathullabad", CityId = 11 },
            new Area { Id = 26, Name = "Modern Mor", CityId = 12 },
            new Area { Id = 27, Name = "Ganginarpar", CityId = 13 }
        );
    }

}