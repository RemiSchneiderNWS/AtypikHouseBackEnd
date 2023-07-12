namespace Domain
{
    public class Advert : DBModel
    {
    
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Tenants { get; set; }    
        public User User { get; set; } = null!;
        public Boolean Status { get; set; }
        public Boolean Up { get; set; }

        public int CriLimit { get; set; }
        public DateTime Created_at { get; set; }
        public int Price { get; set; }
        public string Adress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int Postal { get; set; }   
        public string Describe { get; set;} = string.Empty;
    }
}
