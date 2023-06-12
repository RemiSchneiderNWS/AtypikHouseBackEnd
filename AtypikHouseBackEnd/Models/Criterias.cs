using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtypikHouseBackEnd.Models
{
    [Table("criteria")]
    public class Criterias
    {
        [Key]
        public int cri_id { get; set; }
        public string cri_name { get; set; }

    }
}
