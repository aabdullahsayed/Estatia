using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estatia.Models;
public enum ListingType
{
    Rent,Sell
}
public class Property {
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Title { get; set; }
    
    [Required]
    [Range(typeof(decimal),"1","79228162514264337593543950335", ErrorMessage = "Price must be greater than zero")]
    public decimal Price { get; set; }
    
    [Required]
    public ListingType Type { get; set; }
    
    [Required]
    public string Division { get; set; }
    
    [Required]
    public string City { get; set; }
    
    [Required]
    public string Area { get; set; }
    
    public string? ImageUrl { get; set; }
    
    [NotMapped]
    [Required(ErrorMessage = "Upload a Photo")]
    [Display(Name = "Upload Image")]
    public IFormFile? ImageFile { get; set; }
    

    public int AgentId { get; set; }

    [ForeignKey("AgentId")]
    public Agent? Agent { get; set; }
    
}