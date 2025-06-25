using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace RecipePlatform.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}