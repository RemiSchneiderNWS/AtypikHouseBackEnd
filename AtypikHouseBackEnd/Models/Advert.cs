using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace AtypikHouseBackEnd.Models
{
    [Table("Advert")]
    public class Advert
    {
        [Key]   
        public int adv_id { get; set; } 

        public string adv_name { get; set; }
        public string adv_type { get; set; }   
        public int adv_tenants { get; set; }    
        public int adv_user_id { get; set; }
        public Boolean adv_status { get; set; }
        public Boolean adv_up { get; set; }
        public int adv_cri_limit { get; set; }
        public DateTime adv_created_at { get; set; }
        public int adv_price { get; set; }
        public string adv_adress { get; set; }
        public string adv_city { get; set; }
        public int adv_postal { get; set; }
        public int adv_usr_phone { get; set; }
        public int adv_usr_mail { get; set; }
        public string adv_describe { get; set;}


    }
}
