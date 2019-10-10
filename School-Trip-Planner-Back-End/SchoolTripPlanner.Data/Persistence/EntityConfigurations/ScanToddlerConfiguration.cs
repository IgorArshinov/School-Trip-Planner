using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Persistence.EntityConfigurations
{
    class ScanToddlerConfiguration : IEntityTypeConfiguration<ScanToddler>
    {
        public void Configure(EntityTypeBuilder<ScanToddler> builder)
        {
            builder.HasKey(e => new {e.ToddlerId, e.ScanId});
        }
    }
}