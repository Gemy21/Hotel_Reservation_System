using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Train_Project.Entities;

namespace Train_Project.Data.Configurations
{
    public class BuildingConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {

            builder.HasKey(e => e.Id).HasName("PK__Building__3214EC073E7E5F18");

            builder.ToTable("Building");

            builder.Property(e => e.Id).ValueGeneratedOnAdd ();
            builder.Property(e => e.Location)
                    .HasMaxLength(60)
                    .IsUnicode(false);

            builder.HasOne(d => d.Hotel).WithMany(p => p.Buildings)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK__Building__HotelI__398D8EEE");
        }
    }
}
