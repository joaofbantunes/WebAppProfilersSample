using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebAppProfilersSample.Shared.Data.Entities;

namespace WebAppProfilersSample.Shared.Data.Configurations
{
    internal class SomeEntityConfiguration : IEntityTypeConfiguration<SomeEntity>
    {
        public void Configure(EntityTypeBuilder<SomeEntity> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .UseNpgsqlIdentityAlwaysColumn();
        }
    }
}
