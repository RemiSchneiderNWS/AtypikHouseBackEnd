namespace Domain
{
    public class ActivityImage
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public Activity Activity { get; set; } = null!;

    }
}
