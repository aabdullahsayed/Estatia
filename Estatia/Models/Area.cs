using Estatia.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estatia.Models
{
    public class Area
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Area Name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        
        [Display(Name = "City")]
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }
    }
}