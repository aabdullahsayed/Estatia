
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    [Range(1,double.MaxValue, ErrorMessage = "Price must be greater than zero")]
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
    public IFormFile ImageFile { get; set; }
    
    
}