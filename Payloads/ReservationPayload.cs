using System.ComponentModel.DataAnnotations;
using System.Data;
using Domain;


namespace Payloads
{
    // Copie des variables de User
    public class ReservationPayload
    {
       
        [Required]
        public int advertId { get; set; }

        [Required]
        public DateTime DateStart { get; set; }
        [Required]
        public DateTime DateEnd { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public Boolean Payment { get; set; }
        [Required]
        public string PaymentTimer { get; set; } = string.Empty;
        [Required]
        public int AdvTenants { get; set; }
        [Required]
        public Boolean DelTenant { get; set; }
        [Required]
        public Boolean DelOwner { get; set; }
    }

   
}