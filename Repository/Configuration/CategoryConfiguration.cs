using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData
            (
                new Category
                {
                    Id = new Guid("9e768b71-f27d-426c-ac3d-41e7a7c26729"),
                    Name = "GPU",
                    Description = "A graphics card (also called a video card)" +
                    "is a computer expansion card that " +
                    "generates a feed of graphics output to a display device such as a monitor. " +
                    "Essential part for gaming."
                }
            );
        }
    }
}
