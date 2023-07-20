using System.ComponentModel.DataAnnotations;
using System.Data;
using Domain;


namespace Payloads
{
    // Copie des variables de User
    public class ActivityPayload
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int AdvertId { get; set; }

        [Required]
        public string Adress { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Code postal requis")]
        [StringLength(5 , ErrorMessage = "Code postal Invalide")]
        public string Postal { get; set; } = string.Empty;
        [Required]
        public string Describe { get; set; } = string.Empty;
        [Required]
        public int UserId {get; set; }
      
    }

   
}