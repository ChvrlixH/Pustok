using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Models;

namespace WebAPI.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
           builder.Property(s=> s.Title).IsRequired(true).HasMaxLength(100);
           builder.Property(s=> s.Description).IsRequired(true).HasMaxLength(100);
           builder.Property(s=> s.Image).IsRequired(true).HasMaxLength(500);
        }
    }
}
