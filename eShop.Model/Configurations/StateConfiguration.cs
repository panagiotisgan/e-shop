using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Model.Configurations
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.HasOne(x=>x.Country)
                 .WithMany(x=>x.States)
                 .HasForeignKey(x=>x.CountryId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(State.NameMaxLength);
        }
    }
}
