using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtypikHouseBackEnd.Models
{
    [Table("reserves")]
    public class reserves
    {
        [Key]
        public int res_id { get; set; }

        public int res_usr_id { get; set; }
        public string res_usr_mail { get; set; }
        public int res_usr_phone { get; set; }
        public int res_adv_id { get; set; }
        public string res_adv_name { get; set; }
        public DateTime res_date_start { get; set; }
        public DateTime res_date_end { get; set; }
        public DateTime res_created_at { get;set; }
        public int res_adv_price { get; set; }
        public Boolean res_payment { get; set; }
        public string res_payment_timer {get; set; }
        public int res_adv_tenants { get; set; }
        public Boolean res_del_tenant { get; set; }
        public Boolean res_del_owner { get; set; }

    }
}
