using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Train_Project.Entities;

namespace Train_Project.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("Customer");

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(x => x.Location)
                .HasMaxLength(40)
                .IsUnicode(false);

            builder.Property(x => x.Email)
                .HasMaxLength(40)
                .IsUnicode(false);

            builder.Property(x => x.Phone);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasIndex(x => x.UserId)
                .IsUnique();

            builder.HasOne(x => x.User)
                .WithOne(x => x.Customer)
                .HasForeignKey<Customer>(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}