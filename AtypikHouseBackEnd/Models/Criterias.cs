using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtypikHouseBackEnd.Models
{
    [Table("criteria")]
    public class Criterias
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
