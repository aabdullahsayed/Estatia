using Estatia.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Estatia.Models;

    public class Division
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Division Name is required")]
        [StringLength(100)]
        public string Name { get; set; }

       

        public List<City> Cities { get; set; } = new List<City>();
    }
