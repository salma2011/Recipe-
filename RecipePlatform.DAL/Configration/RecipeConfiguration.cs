using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipePlatform.Models;
namespace RecipePlatform.DAL.Configration
{
  
    

    namespace RecipePlatform.DAL.Configurations
    {
        public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
        {
            public void Configure(EntityTypeBuilder<Recipe> builder)
            {
                builder.HasKey(r => r.Id);

                builder.Property(r => r.Title)
                       .IsRequired()
                       .HasMaxLength(100);

                builder.Property(r => r.Description)
                       .HasMaxLength(500);

                builder.Property(r => r.Ingredients)
                       .HasMaxLength(1000);

                builder.Property(r => r.Instructions)
                       .HasMaxLength(2000);

                builder.HasOne(r => r.User)
                       .WithMany(u => u.Recipes)
                       .HasForeignKey(r => r.UserId);

                builder.HasOne(r => r.Category)
                       .WithMany(c => c.Recipes)
                       .HasForeignKey(r => r.CategoryId);
            }
        }
    }
}