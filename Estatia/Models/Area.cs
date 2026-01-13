using System.ComponentModel.DataAnnotations;

public class Area
{
    
    private int _id;
    private string _name;
    private int _cityId;
    private City _city;

    [Key]
    public int Id
    {
        get { return _id; }
        private set { _id = value; }
    }

    [Required]
    [StringLength(100)]
    public string Name
    {
        get { return _name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Area name is required");

            _name = value;
        }
    }

 
    [Required]
    public int CityId
    {
        get { return _cityId; }
        set { _cityId = value; }
    }

    public City City
    {
        get { return _city; }
        set { _city = value; }
    }
}
