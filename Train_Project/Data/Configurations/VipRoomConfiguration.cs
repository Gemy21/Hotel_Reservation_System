using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Train_Project.Entities;
namespace Train_Project.Data.Configurations
{
    public class VipRoomConfiguration : IEntityTypeConfiguration<VipRoom>
    {
        public void Configure(EntityTypeBuilder<VipRoom> builder)
        {
            builder.ToTable("VipRoom");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.LivingArea).HasColumnType("decimal(6,2)");
            builder.Property(e => e.LateCheckOutFee).HasColumnType("decimal(8,2)");

            builder.HasOne(v => v.Room)
                .WithOne(r => r.VipRoom)
                .HasForeignKey<VipRoom>(v => v.Id)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
