namespace eShop.Model
{
    public class City:BaseEntity
    {
        public const int NameMaxLength = 120;

        public string Name { get; set; }
        public long StateId { get; set; }
        public State State { get; set; }
    }
}