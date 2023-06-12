using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtypikHouseBackEnd.Models
{
    [Table("commentarys")]
    public class Commentarys
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public int AdvId { get; set; }
        public int UsrId { get; set; }
        public string UsrFirstName { get; set; }
        public string UsrLastName { get; set; }
    }
}
