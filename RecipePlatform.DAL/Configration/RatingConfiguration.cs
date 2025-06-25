
using RecipePlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



    namespace RecipePlatform.DAL.Configurations
    {
        public class RatingConfiguration : IEntityTypeConfiguration<Rating>
        {
            public void Configure(EntityTypeBuilder<Rating> builder)
            {
                builder.HasKey(r => r.Id);

                builder.Property(r => r.Score)
                       .IsRequired();

                builder.Property(r => r.Comment)
                       .HasMaxLength(500);

            builder.HasOne(r => r.Recipe)
       .WithMany(re => re.Ratings)
       .HasForeignKey(r => r.RecipeId)
       .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.User)
                   .WithMany(u => u.Ratings)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
        }
    }
