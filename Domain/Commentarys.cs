namespace Domain
{
    public class Commentarys
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public Advert Advert { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
