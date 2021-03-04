using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Model.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        { 
            builder.HasOne(x=>x.Country)
                .WithOne()
                .HasForeignKey<User>(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x=>x.Credential)
            .WithOne()
            .HasForeignKey<User>(x => x.CredentialId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(User.NamesLength);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(User.NamesLength);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(User.PhoneMaxLength);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(User.EmailMaxLength);
            builder.Property(x => x.AddressNo1).IsRequired().HasMaxLength(User.AddressMaxLength);
            builder.Property(x => x.AddressNo2).HasMaxLength(User.AddressMaxLength).IsRequired(false);
            builder.Property(x => x.VATNumber).IsRequired(false).HasMaxLength(User.VATLength);
        }
    }
}
