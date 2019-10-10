using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Persistence.EntityConfigurations
{
    class SchoolTripConfiguration : IEntityTypeConfiguration<SchoolTrip>
    {
        public void Configure(EntityTypeBuilder<SchoolTrip> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Teacher)
                .WithMany()
                .HasForeignKey(e => e.TeacherId);
        }
    }
}