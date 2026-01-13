using Estatia.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Division
{
   

    private int _id;
    private string _name;
    private List<City> _cities;

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
                throw new ArgumentException("Division name is required");

            _name = value;
        }
    }


    public List<City> Cities
    {
        get { return _cities; }
        set { _cities = value; }
    }

    public Division()
    {
        _cities = new List<City>();
    }
}
