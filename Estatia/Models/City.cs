using Estatia.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace Estatia.Models;

    public class City
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "City Name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        
        [Display(Name = "Division")]
        public int DivisionId { get; set; }


        [ForeignKey("DivisionId")]
        public Division Division { get; set; }

 
        public List<Area> Areas { get; set; } = new List<Area>();
    }

