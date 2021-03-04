using eShop.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<eShop.Model.Image>
    {
        public void Configure(EntityTypeBuilder<eShop.Model.Image> builder)
        {
            builder.Property(p => p.ImagePath).IsRequired(false);
            builder.Property(p => p.ProductId).IsRequired();
            builder.Property(p => p.TitleAttribute).HasMaxLength(15);

            builder.HasOne<Product>()
                   .WithMany(x=>x.Images)
                   .OnDelete(DeleteBehavior.Restrict);                    
        }
    }
}
