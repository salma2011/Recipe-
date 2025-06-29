using Microsoft.AspNetCore.Mvc.Rendering;
using RecipePlatform.Models;
using System.ComponentModel.DataAnnotations;

namespace RecipePlatform.PL.ViewModels
{
    public class RecipeFormViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Ingredients { get; set; }

        [Required]
        public string Instructions { get; set; }

        [Required]
        public int PreparationTime { get; set; }

        [Required]
        public int CookingTime { get; set; }

        [Required]
        public int Servings { get; set; }

        [Required]
        public DifficultyLevel Difficulty { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int? CategoryId { get; set; }

        public IFormFile? ImageFile { get; set; }

        public List<SelectListItem> Categories { get; set; } = new();
    }
}
