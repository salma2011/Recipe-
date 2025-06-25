namespace RecipePlatform.Models
{
    public class Rating : BaseEntity
    {
        public int Score { get; set; }
        public string Comment { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}