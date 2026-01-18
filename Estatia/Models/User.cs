using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estatia.Models;

public enum UserRole
{
    User,  
    Agent, 
    Admin  
}

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int Id { get; set; }

    // === NEW IDENTITY FIELDS ===
    [Required(ErrorMessage = "Please enter your full name")]
    [StringLength(100)]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } // e.g. "John Doe"

    [Required(ErrorMessage = "Phone number is required for contact")]
    [Phone]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } // e.g. "01712-345678"

 
    // === EXISTING FIELDS ===
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; }

    [Required]
    public UserRole Role { get; set; }
    
    
    public  ICollection<Property>? Properties { get; set; }
}