using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Persistence.EntityConfigurations
{
    class ToddlerConfiguration : IEntityTypeConfiguration<Toddler>
    {
        public void Configure(EntityTypeBuilder<Toddler> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
        }
    }
}
