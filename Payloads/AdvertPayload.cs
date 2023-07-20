using System.ComponentModel.DataAnnotations;
using System.Data;
using Domain;


namespace Payloads
{
    // Copie des variables de User
    public class AdvertPayload
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Type { get; set; } = string.Empty;
        [Required]
        public int Tenants { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public Boolean Status { get; set; }

        [Required] 
        public Boolean Up { get; set; }
        [Required]
        public int CriLimit { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Adress { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Code postal requis")]
        [StringLength(5 , ErrorMessage = "Code postal Invalide")]
        public string Postal { get; set; } = string.Empty;
        [Required]
        public string Describe { get; set; } = string.Empty;       
      
    }
   
}