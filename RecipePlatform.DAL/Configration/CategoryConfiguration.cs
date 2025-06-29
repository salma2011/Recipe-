using global::RecipePlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipePlatform.Models;

namespace RecipePlatform.DAL.Configration
{


    namespace RecipePlatform.DAL.Configurations
    {
        public class CategoryConfiguration : IEntityTypeConfiguration<Category>
        {
            public void Configure(EntityTypeBuilder<Category> builder)
            {
               // builder.HasKey(c => c.Id);

                builder.Property(c => c.Name)
                       .IsRequired()
                       .HasMaxLength(50);
            }
        }
    }
}