using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Ambev.DeveloperEvaluation.Domain.Entities.User;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public partial class UserConfiguration
{
    public class SalesConfiguration : IEntityTypeConfiguration<Sales>{
        public void Configure(EntityTypeBuilder<Sales> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(s => s.SaleDate).HasColumnType("timestamp");
            builder.Property(s => s.Customer).HasColumnType("uuid");
            builder.Property(s => s.CustomerName).HasMaxLength(50);
            builder.Property(s => s.TotalSaleAmount).HasColumnType("decimal(10,2)");
            builder.Property(s => s.Branch).HasMaxLength(50);
            builder.Property(s => s.TotalAmount).HasColumnType("decimal(10,2)");
            builder.Property(s => s.IsCancelled).HasColumnType("boolean");

            builder.HasMany(s => s.Items)
                .WithOne()
                .HasForeignKey(i => i.SaleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        
    }
}
