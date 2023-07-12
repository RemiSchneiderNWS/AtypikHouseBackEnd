namespace Domain
{
    public class AdvertImage : DBModel
    {
    
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public Advert Advert{ get; set; } = null!;

    }
}
