using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P013EStore.Core.Entities;

namespace P013EStore.Data.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Image).HasMaxLength(100);
            builder.Property(x => x.ProductCode).HasMaxLength(50);
            // FluentAPI ile class lar arası ilişki kurma
            builder.HasOne(x => x.Brand).WithMany(x => x.Products).HasForeignKey(f => f.BrandId); // Brand clas ı ile Products class ı arasında 1 e çok ilişki vardır
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(c => c.CategoryId); // Category clas ı ile Products class ı arasında 1 e çok ilişki vardır
        }
    }
}
