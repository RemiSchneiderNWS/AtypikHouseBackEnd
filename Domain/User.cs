namespace Domain
{
    public class User : DBModel
    {
        public string Mail { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public Role Role { get; set; }

        public string FirstName {get;set;}
        
        public string LastName { get; set;}

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
        public string ExpiresIn { get; set; }
    }
}
