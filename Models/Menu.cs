namespace HappyNoodles_ManagementApp.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}