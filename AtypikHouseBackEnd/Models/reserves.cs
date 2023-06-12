using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtypikHouseBackEnd.Models
{
    [Table("reserves")]
    public class reserves
    {
        [Key]
        public int Id { get; set; }

        public int UsrId { get; set; }
        public string UsrMail { get; set; }
        public int UsrPhone { get; set; }
        public int AdvId { get; set; }
        public string AdvName { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime CreatedAt { get;set; }
        public int AdvPrice { get; set; }
        public Boolean Payment { get; set; }
        public string PaymentTimer {get; set; }
        public int AdvTenants { get; set; }
        public Boolean DelTenant { get; set; }
        public Boolean DelOwner { get; set; }

    }
}
