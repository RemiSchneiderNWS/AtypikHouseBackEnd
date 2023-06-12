using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtypikHouseBackEnd.Models
{
    [Table("criteriadvert")]
    public class criteriadvert
    {
         [Key] 
         public int Id { get; set; }
         public int CriId { get; set; } 
         public int AdvId { get; set; }

    }
}
