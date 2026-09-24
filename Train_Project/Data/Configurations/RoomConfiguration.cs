using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Train_Project.Entities;
namespace Train_Project.Data.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Room__3214EC07E8473B6D");

            builder.ToTable("Room");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.IsAvailable).HasDefaultValue(true);

            builder.HasOne(d => d.Building).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.BuildingId)
                .HasConstraintName("FK__Room__BuildingId__3E52440B");

            builder.HasOne(d => d.Hotel).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK__Room__HotelId__3D5E1FD2");

            builder.Property(e => e.RoomType)
                .HasConversion<string>()
                .HasMaxLength(50);


        }
    }
}
