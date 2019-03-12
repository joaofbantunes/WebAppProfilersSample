using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppProfilersSample.Shared.Data.Entities;

namespace WebAppProfilersSample.Shared.Data.Configurations
{
    internal class SomeOtherEntityConfiguration : IEntityTypeConfiguration<SomeOtherEntity>
    {
        public void Configure(EntityTypeBuilder<SomeOtherEntity> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .UseNpgsqlIdentityAlwaysColumn();

            builder
                .HasOne(s => s.Some)
                .WithMany(s => s.Others);
        }
    }
}
