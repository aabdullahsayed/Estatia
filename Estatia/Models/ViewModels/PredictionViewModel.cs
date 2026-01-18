using System.ComponentModel.DataAnnotations;

namespace Estatia.Models.ViewModels
{
    public class PredictionViewModel
    {
        [Required]
        [Display(Name = "Size (Sq Ft)")]
        public float SizeSqFt { get; set; }

        [Required]
        [Display(Name = "City")]
        public int CityCode { get; set; } // 0=Dhaka, 1=Chittagong

        [Required]
        [Display(Name = "Listing Type")]
        public int TypeCode { get; set; } // 0=Sell, 1=Rent

        public float? PredictedPrice { get; set; }
    }
}