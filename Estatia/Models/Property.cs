
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
    
    
    public string Title { get; set; }
    public decimal Price { get; set; }
    public ListingType Type { get; set; }
    public string Division { get; set; }
    public string City { get; set; }
    public string Area { get; set; }
    public string ImageUrl { get; set; }
    
}