using System;
using System.Collections.Generic;

namespace RecipePlatform.Models
{
    public enum DifficultyLevel { Easy, Medium, Hard }

    public class Recipe : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public int PreparationTime { get; set; }
        public int CookingTime { get; set; }
        public int Servings { get; set; }
        public DifficultyLevel Difficulty { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? ImagePath { get; set; }
    }
}
