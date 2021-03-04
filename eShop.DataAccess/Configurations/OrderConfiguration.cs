using eShop.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.OrderStatus).IsRequired();
            builder.Property(o => o.Order_Date).IsRequired();
            builder.Property(o => o.Delivered_Date).IsRequired(false);
            builder.Property(o => o.Total_Cost).IsRequired();
            builder.Property(o => o.Invoice).IsRequired();

            builder.HasOne(x=>x.User)
                .WithMany(x=>x.Orders)
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
