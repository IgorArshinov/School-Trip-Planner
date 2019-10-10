using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Persistence.EntityConfigurations
{
    class SchoolTripToddlerConfiguration : IEntityTypeConfiguration<SchoolTripToddler>
    {
        public void Configure(EntityTypeBuilder<SchoolTripToddler> builder)
        {
            builder.HasKey(e => new
            {
                e.SchoolTripId,
                e.ToddlerId
            });
            builder.HasOne(e => e.SchoolTrip)
                .WithMany(e => e.SchoolTripToddlers)
                .HasForeignKey(e => e.SchoolTripId);
        }
    }
}