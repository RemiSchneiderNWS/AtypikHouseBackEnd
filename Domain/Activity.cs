
namespace Domain
{
    public class Activity : DBModel
    {
        public string Name { get; set; } = string.Empty;
       
        public Advert Advert { get; set; } = null!;
        public string Adress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Postal { get; set; } = string.Empty;
        public string Describe { get; set; } = string.Empty;
        public User User { get; set; } = null!;
    }
}
