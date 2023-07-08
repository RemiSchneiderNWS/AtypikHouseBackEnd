namespace Domain
{
    public class User : DBModel
    {
        public string Mail { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public Role Role { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;
        public string ExpiresIn { get; set; } = string.Empty;
    }
}
