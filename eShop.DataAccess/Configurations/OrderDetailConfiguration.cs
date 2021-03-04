using eShop.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.HasOne(x=> x.Order )
                .WithMany(x=>x.OrderDetails)
                .HasForeignKey(x=> x.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(od => od.Product_Name).IsRequired().HasMaxLength(OrderDetails.ProductNameMaxLength);

        }
    }
}
