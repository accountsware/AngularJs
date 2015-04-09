using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Angular.Data.Modals;

namespace Angular.Data.Mappings
{
    public class CurrentProductListMap : EntityTypeConfiguration<CurrentProductList>
    {
        public CurrentProductListMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductID, t.ProductName });

            // Properties
            this.Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            // Table & Column Mappings
            this.ToTable("CurrentProductList");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
        }
    }
}
