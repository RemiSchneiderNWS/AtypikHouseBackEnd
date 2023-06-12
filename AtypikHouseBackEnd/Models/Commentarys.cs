using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtypikHouseBackEnd.Models
{
    [Table("commentarys")]
    public class Commentarys
    {
        [Key]
        public int com_id { get; set; }
        public string com_text { get; set; }
        public int com_adv_id { get; set; }
        public int com_usr_id { get; set; }
        public string com_usr_firstName { get; set; }
        public string com_usr_lastName { get; set; }
    }
}
