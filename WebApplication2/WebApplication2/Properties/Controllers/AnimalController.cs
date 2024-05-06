using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Properties.Model;

namespace WebApplication2.Properties.Controllers;


[ApiController]
[Route("api/animals")]
public class AnimalController:ControllerBase
{

    
    [HttpGet]
    public IActionResult getAnimals()
    {

        var con = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=APBD;Trusted_Connection=True;");
        con.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = con;



        cmd.CommandText = "SELECT IdAnimal, Name, Description, Category, Area FROM ANIMAL";
        

            var dr = cmd.ExecuteReader();
            var animals = new List<Animal>();

            while (dr.Read())
            {
                var an = new Animal
                {
                
                    IdAnimal = (int)dr["IdAnimal"],
                    name = dr["Name"].ToString(),
                    description = dr["Description"].ToString(),
                    category = dr["Category"].ToString(),
                    area = dr["Area"].ToString()

                };
            
                animals.Add(an);


            }
        

            con.Close();

            return Ok(animals);
        
        
        
    }
    
    
    
    
    
    


    [HttpGet("{kolumna}")]
    public IActionResult getAnimalsByOrder(string kolumna)
    {

        var con = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=APBD;Trusted_Connection=True;");
        con.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = con;

        kolumna = kolumna.ToLower();

        if (kolumna.Equals("name") || kolumna.Equals("idanimal") || kolumna.Equals("description") ||
            kolumna.Equals("category") || kolumna.Equals("area"))
        {
           
            cmd.CommandText = "SELECT IdAnimal, Name, Description, Category, Area FROM ANIMAL ORDER BY "+kolumna;
        

            var dr = cmd.ExecuteReader();
            var animals = new List<Animal>();

            while (dr.Read())
            {
                var an = new Animal
                {
                
                    IdAnimal = (int)dr["IdAnimal"],
                    name = dr["Name"].ToString(),
                    description = dr["Description"].ToString(),
                    category = dr["Category"].ToString(),
                    area = dr["Area"].ToString()

                };
            
                animals.Add(an);


            }
        

            con.Close();

            return Ok(animals);
        }
        else
        {
            return BadRequest("Order by is incorrect");
        }
        

        
    }

    [HttpPost]
    public IActionResult addStudent(Animal animal)
    {
        var con = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=APBD;Trusted_Connection=True;");

        con.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "insert into animal (Name, Description, Category, Area) values (@name, @desc, @cat, @area)";
        cmd.Parameters.AddWithValue("@name", animal.name);
        cmd.Parameters.AddWithValue("@desc", animal.description);
        cmd.Parameters.AddWithValue("@cat", animal.category);
        cmd.Parameters.AddWithValue("@area", animal.area);

        var affectedRows = cmd.ExecuteNonQuery();
        
        
        con.Close();

        return Ok("Affrcted rows: "+affectedRows);
    }




    [HttpPut("{id:int}")]
    public IActionResult Update(int id, Animal animal)
    {
        
        var con = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=APBD;Trusted_Connection=True;");

        con.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "update animal set Name = @name, Description = @desc, Category = @cat, Area = @area where idanimal = @id";
        cmd.Parameters.AddWithValue("@name", animal.name);
        cmd.Parameters.AddWithValue("@desc", animal.description);
        cmd.Parameters.AddWithValue("@cat", animal.category);
        cmd.Parameters.AddWithValue("@area", animal.area);
        cmd.Parameters.AddWithValue("@id", id);

        var affected = cmd.ExecuteNonQuery();
        con.Close();

        if (affected == 0)
        {
            return NotFound("Animal with id: "+id+" does not exist");
        }
        
        return Ok("Affected rows: " + affected);
    }



    [HttpDelete("{id:int}")]
    public IActionResult deleteAnimal(int id)
    {
        var con = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=APBD;Trusted_Connection=True;");

        con.Open();

        using var cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandText = "delete from animal where idanimal = @id";
        cmd.Parameters.AddWithValue("@id", id);
        var affection = cmd.ExecuteNonQuery();
        con.Close();
        
        return Ok(affection + " row/s were deleted");
    }
    
    

}