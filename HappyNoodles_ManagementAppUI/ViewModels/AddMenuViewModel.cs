using System.ComponentModel.DataAnnotations;

namespace HappyNoodles_ManagementApp.ViewModels
{
    public class AddMenuViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
