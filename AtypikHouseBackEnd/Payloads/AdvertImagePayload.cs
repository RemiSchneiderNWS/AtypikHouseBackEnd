using System.ComponentModel.DataAnnotations;
using System.Data;
using Domain;


namespace Payloads
{
   
    public class AdvertImagePayload
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Order { get; set; }

        [Required]
        public int AdvertId { get; set; }
      
    }

   
}