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
            //Create UniqueField in DB
            builder.HasIndex(x=>x.Username).IsUnique();

            builder.Property(cr => cr.Password).IsRequired();

            builder.Property(cr => cr.Salt).IsRequired();
        }
    }
}
