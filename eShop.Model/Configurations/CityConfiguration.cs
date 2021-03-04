using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Model.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasOne<State>(x=>x.State)                
                .WithMany(x=>x.Cities)
                .HasForeignKey(x=>x.StateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(City.NameMaxLength);
            builder.Property(x => x.ZipCode).IsRequired().HasMaxLength(City.ZipCodeMaxLength);

            
        }
    }
}
