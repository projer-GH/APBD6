using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Properties.Model;

public class Animal
{
    public int IdAnimal { get; set; }
    [Required]
    public string name{ get; set; }
    [Required]
    public string description{ get; set; }
    [Required]
    public string category{ get; set; }
    [Required]
    public string area{ get; set; }

    

}