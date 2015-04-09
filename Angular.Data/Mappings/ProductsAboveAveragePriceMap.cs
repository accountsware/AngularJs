using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using AngularJs.Core.Modals;

namespace Angular.Data.Mappings
{
    public class ProductsAboveAveragePriceMap : EntityTypeConfiguration<ProductsAboveAveragePrice>
    {
        public ProductsAboveAveragePriceMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductName);

            // Properties
            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            // Table & Column Mappings
            this.ToTable("ProductsAboveAveragePrice");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
        }
    }
}
