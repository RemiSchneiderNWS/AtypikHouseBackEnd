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
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{4,}$", ErrorMessage = "Le mot de passe doit avoir au moins 4 caractères, une majuscule et un chiffre.")]
        public string Password { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Téléphone non valide")]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty; 
        
        public User iniUser()
        {
            User user = new User();
            user.Mail = Mail; 
            user.Password = Password;
            user.Phone = Phone;
            user.Role = 0;
            user.FirstName = FirstName;
            user.LastName = LastName; 
            return user;
        }
    }

   
}