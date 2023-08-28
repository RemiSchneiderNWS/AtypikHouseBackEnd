namespace Domain
{
    public class Advert : DBModel
    {
    
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Tenants { get; set; }    
        public User User { get; set; } = null!;
        public bool Status { get; set; }
        public bool Up { get; set; }
        public int CriLimit { get; set; }
        public DateTime Created_at { get; set; }
        public int Price { get; set; }
        public string Adress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Postal { get; set; } = string.Empty;
        public string Describe { get; set;} = string.Empty;
    }
}
