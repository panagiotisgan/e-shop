using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Model.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Code).IsRequired().HasMaxLength(Product.CodeMaxLength);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(Product.NameMaxLength);
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.StockQty).IsRequired();
            builder.Property(p => p.Image).IsRequired(false);

            builder.HasOne(x=>x.Category)
                .WithMany(x=>x.Products)
                .HasForeignKey(x=>x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
