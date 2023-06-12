using System.ComponentModel.DataAnnotations.Schema;

namespace AtypikHouseBackEnd.Models
{
    [Table("activityImage")]
    public class ActivityImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }  
        public int ActivityId { get; set; }

    }
}
