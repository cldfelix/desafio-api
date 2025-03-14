using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Ambev.DeveloperEvaluation.Domain.Entities.User;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public partial class UserConfiguration
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>{
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.HasOne(i => i.Product)
                .WithMany()
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Property(i => i.ProductName).IsRequired().HasMaxLength(50);
            
            builder.Property(i => i.Quantity).HasColumnType("int");
            builder.Property(i => i.UnitPrice).HasColumnType("decimal(10,2)");
            builder.Property(i => i.Discount).HasColumnType("decimal(10,2)");
            builder.Property(i => i.TotalAmountItem).HasColumnType("decimal(10,2)");
         
        }
        
    }
}
