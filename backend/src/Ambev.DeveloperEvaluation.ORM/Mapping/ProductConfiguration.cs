using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Ambev.DeveloperEvaluation.Domain.Entities.User;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public partial class UserConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).HasMaxLength(100);
            builder.Property(p => p.Price).HasColumnType("decimal(10,2)");
            builder.Property(p => p.Stock).HasColumnType("int");

        }
        
    }
}
