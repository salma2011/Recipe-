using System.Collections.Generic;

namespace RecipePlatform.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}