namespace HappyNoodles_ManagementApp.ViewModels.Category
{
    public class CategoryViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public CategoryMenuViewModel Menu { get; set; } = new CategoryMenuViewModel();
    }
}
