using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtypikHouseBackEnd.Models
{
    [Table("Activity")]
    public class Activity
    {
       [Key]
       public int act_id { get; set; }
       public string act_name { get; set;}
       public int act_adv_id { get; set; }
       public string  act_adress{ get; set; }
       public string  act_city{ get; set; }
       public int act_postal { get; set; } // string ?
       public string act_describe { get; set; }
       public int act_usr_id { get; set; } 
    }
}
