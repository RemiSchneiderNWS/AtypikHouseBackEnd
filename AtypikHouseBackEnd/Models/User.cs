using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtypikHouseBackEnd.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        public int usr_id { get; set; }
        public string usr_mail { get; set; }

        public string usr_password { get; set; }

        public int usr_phone { get; set; }

        public int usr_rol_id { get; set; }

        public string usr_firstName {get;set;}
        
        public string usr_lastName { get; set;}

        public string usr_access_token { get; set; }

        public string usr_refresh_token { get; set; }
        public string usr_expires_in { get; set; }
    }
}
