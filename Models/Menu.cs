namespace HappyNoodles_ManagementApp.Models
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}