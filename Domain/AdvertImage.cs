namespace Domain
{
    public class AdvertImage
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public Advert Advert{ get; set; } = null!;

    }
}
