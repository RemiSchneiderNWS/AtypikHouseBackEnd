using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtypikHouseBackEnd.Models
{
    [Table("criteriadvert")]
    public class criteriadvert
    {
         [Key] 
         public int ria_id { get; set; }
         public int ria_cri_id { get; set; } 
         public int ria_adv_id { get; set; }

    }
}
