using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Train_Project.Entities;

namespace Train_Project.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Customer__3214EC07858D8D4E");

            builder.ToTable("Customer");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false);
            builder.Property(e => e.Location)
                .HasMaxLength(40)
                .IsUnicode(false);
            builder.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
            builder.Property(e => e.Password)
                .HasMaxLength(256   )
                .IsUnicode(false);
            builder.Property(e => e.Username)
                .HasMaxLength(40)
                .IsUnicode(false);
        }
    }
}
