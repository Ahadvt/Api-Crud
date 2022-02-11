using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data.Canfigurations
{
    public class CategoryCanfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(40).IsRequired(true);
            builder.Property(c => c.CreateDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(c => c.UpdateDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(c => c.Image).HasMaxLength(100).IsRequired(false);
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);

        }
    }
}
