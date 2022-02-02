using eShop.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess.Configurations
{
    public class CredentialConfiguration : IEntityTypeConfiguration<Credential>
    {
        public void Configure(EntityTypeBuilder<Credential> builder)
        {
            builder.Property(cr => cr.Username).IsRequired()
                  .HasMaxLength(50);

            builder.Property(cr => cr.Password).IsRequired();

            builder.Property(cr => cr.Salt).IsRequired();
        }
    }
}
