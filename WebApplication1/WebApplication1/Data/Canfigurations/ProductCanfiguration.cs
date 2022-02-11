using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data.Canfigurations
{
    public class ProductCanfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired(true);
            builder.Property(p => p.SalePrice).HasColumnType("decimal(18,2)").IsRequired(true);
            builder.Property(p => p.CostPrice).HasColumnType("decimal(18,2)").IsRequired(true);
            builder.Property(p => p.CreateDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(p => p.UpdateDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
        }
    }
}
