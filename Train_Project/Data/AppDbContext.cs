using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Train_Project.Authentication.AuthEntity;
using Train_Project.Entities;

namespace Train_Project.Data;

public partial class AppDbContext : DbContext
{


    public virtual DbSet<Building> Buildings { get; set; }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomComponent> RoomComponents { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    public virtual DbSet<VipRoom> VipRooms { get; set; }

    public virtual DbSet<LateCheckOutRequest> LateCheckOutRequests { get; set; }

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<VipRoom>(builder =>
        {
          
        });
        modelBuilder.Entity<LateCheckOutRequest>(builder =>
        {
          
        });
    }
}
