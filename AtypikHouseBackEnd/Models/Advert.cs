using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace AtypikHouseBackEnd.Models
{
    [Table("Adverts")]
    public class Advert
    {
        [Key]   
        public int Id { get; set; } 

        public string Name { get; set; }
        public string Type { get; set; }   
        public int Tenants { get; set; }    
        public int User_id { get; set; }
        public Boolean Status { get; set; }
        public Boolean Up { get; set; }
        public int CriLimit { get; set; }
        public DateTime Created_at { get; set; }
        public int Price { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public int Postal { get; set; }
        public int UsrPhone { get; set; }
        public int UsrMail { get; set; }
        public string Describe { get; set;}


    }
}
