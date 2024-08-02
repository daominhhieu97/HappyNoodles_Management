namespace HappyNoodles_ManagementApp.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public Guid MenuId { get; set; }
        public Menu Menu { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}