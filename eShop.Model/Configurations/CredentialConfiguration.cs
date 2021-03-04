using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Model.Configurations
{
    public class CredentialConfiguration : IEntityTypeConfiguration<Credential>
    {
        public void Configure(EntityTypeBuilder<Credential> builder)
        {
          
        }
    }
}
