using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Train_Project.Entities;

namespace Train_Project.Data.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Hotel__3214EC079C0AA240");

            builder.ToTable("Hotel");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
