using System.ComponentModel.DataAnnotations;
using System.Data;
using Domain;


namespace Payloads
{
    // Copie des variables de User
    public class CriteriaAdvertPayload
    {
        [Required]
        public int CriteriaId { get; set; }

        [Required]
        public int AdvertId { get; set; }
      
    }

   
}