namespace HappyNoodles_ManagementApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public ICollection<FoodItem> FoodItems { get; set; }
    }
}