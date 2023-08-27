namespace Domain
{
    public class ActivityImage : DBModel
    {
       
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public Activity Activity { get; set; } = null!;

    }
}
