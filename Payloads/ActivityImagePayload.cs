using System.ComponentModel.DataAnnotations;
using System.Data;
using Domain;


namespace Payloads
{
    // Copie des variables de User
    public class ActivityImagePayload
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Order { get; set; }

        [Required]
        public int ActivityId {get; set; }
      
    }

   
}