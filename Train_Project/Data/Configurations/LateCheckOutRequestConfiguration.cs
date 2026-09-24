using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Train_Project.Entities;
namespace Train_Project.Data.Configurations
{
    public class LateCheckOutRequestConfiguration : IEntityTypeConfiguration<LateCheckOutRequest>
    {
        public void Configure(EntityTypeBuilder<LateCheckOutRequest> builder)
        {
            builder.ToTable("LateCheckOutRequest");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.ExtraCharge).HasColumnType("decimal(8,2)");

            builder.HasOne(e => e.Reservation).WithMany()
                .HasForeignKey(e => e.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.VipRoom).WithMany()
                .HasForeignKey(e => e.VipRoomId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
