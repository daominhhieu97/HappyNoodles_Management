﻿using HappyNoodles_ManagementApp.Models.Enums;
using HappyNoodles_ManagementApp.ViewModels.Category;

namespace HappyNoodles_ManagementApp.ViewModels.Item
{
    public class ItemViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; } = default;
        public Guid? CategoryId { get; set; } = default;
        public AvailableStatuses AvailableStatus { get; set; }
        public int RemainingItem { get; set; } = 1;
        public CategoryViewModel? Category { get; set; } = new CategoryViewModel();
        public string? PictureUrl { get; set; }
    }
}
