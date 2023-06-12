using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtypikHouseBackEnd.Models
{
    [Table("Activitys")]
    public class Activity
    {
       [Key]
       public int Id { get; set; }
       public string Name { get; set;}
       public int AdvId { get; set; }
       public string  Adress { get; set; }
       public string  City { get; set; }
       public int Postal { get; set; } 
       public string Describe { get; set; }
       public int UsrId { get; set; } 
    }
}
