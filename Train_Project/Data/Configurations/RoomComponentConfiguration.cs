using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Train_Project.Entities;
namespace Train_Project.Data.Configurations
{
    public class RoomComponentConfiguration : IEntityTypeConfiguration<RoomComponent>
    {
        public void Configure(EntityTypeBuilder<RoomComponent> builder)
        {
            builder
                .HasNoKey()
                .ToTable("RoomComponent");

            builder.HasOne(d => d.Component).WithMany()
                .HasForeignKey(d => d.ComponentId)
                .HasConstraintName("FK__RoomCompo__Compo__4316F928");

            builder.HasOne(d => d.Room).WithMany()
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__RoomCompo__RoomI__4222D4EF");
        }
    }
}

