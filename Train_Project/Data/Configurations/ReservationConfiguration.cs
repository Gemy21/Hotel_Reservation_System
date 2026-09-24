using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Train_Project.Entities;
namespace Train_Project.Data.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Reservat__3214EC07340FBDE2");

            builder.ToTable("Reservation");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Status)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.HasOne(d => d.Customer).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Reservati__Custo__49C3F6B7");

            builder.HasOne(d => d.Hotel).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK__Reservati__Hotel__47DBAE45");

            builder.HasOne(d => d.Room).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__Reservati__RoomI__48CFD27E");
        }
    }
}
