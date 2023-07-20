using System.ComponentModel.DataAnnotations;
using System.Data;
using Domain;


namespace Payloads
{
    // Copie des variables de User
    public class CommentaryPayload
    {
        [Required]
        public string Text { get; set; } = string.Empty;

        [Required]
        public int advertId { get; set; }

        
       
    }

   
}