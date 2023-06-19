using System.ComponentModel.DataAnnotations;
using System.Data;
using Domain;

namespace Payloads
{
    // Copie des variables de User
    public class CreateUserPayload
    {
        [Required(ErrorMessage = "Adresse mail requise"), EmailAddress(ErrorMessage = "Adresse mail non valide.")]
        public string Mail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mot de passe requis") /* , RegularExpression()*/]
        public string Password { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Téléphone non valide")]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;
    }
}