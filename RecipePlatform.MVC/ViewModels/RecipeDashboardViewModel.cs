using RecipePlatform.Models;
using RecipePlatform.PL.ViewModels;

public class RecipeDashboardViewModel
{
    // معلومات المستخدم
    public string UserName { get; set; }
    public string Email { get; set; }

    // نموذج إضافة وصفة
    public RecipeFormViewModel RecipeForm { get; set; }

    // قائمة الوصفات الخاصة بالمستخدم
    public List<Recipe> MyRecipes { get; set; }
}
