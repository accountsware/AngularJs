using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Angular.Data.Modals;

namespace Angular.Data.Mappings
{
    public class CustomerAndSuppliersByCityMap : EntityTypeConfiguration<CustomerAndSuppliersByCity>
    {
        public CustomerAndSuppliersByCityMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CompanyName, t.Relationship });

            // Properties
            this.Property(t => t.City)
                .HasMaxLength(15);

            this.Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.ContactName)
                .HasMaxLength(30);

            this.Property(t => t.Relationship)
                .IsRequired()
                .HasMaxLength(9);

            // Table & Column Mappings
            this.ToTable("CustomerAndSuppliersByCity");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.ContactName).HasColumnName("ContactName");
            this.Property(t => t.Relationship).HasColumnName("Relationship");
        }
    }
}
