namespace Domain
{
    public class Commentarys : DBModel
    {
      
        public string Text { get; set; } = string.Empty;
        public Advert Advert { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
