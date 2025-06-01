using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ReBottle.Models.Data
{
    public class ReBottleContext : DbContext
    {
        public ReBottleContext(DbContextOptions<ReBottleContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(builder => builder.EnableRetryOnFailure());
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RecyclingRecord> RecyclingRecords { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Location> Locations { get; set; }
        //public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ImageStorage> ImagesStorage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-Many: User -> RecyclingRecords
            modelBuilder.Entity<RecyclingRecord>()
                .HasOne(r => r.User)               
                .WithMany(u => u.RecyclingRecords) 
                .HasForeignKey(r => r.UserId);     

            // One-to-Many: RecyclingRecords -> OrderStatus
            //modelBuilder.Entity<RecyclingRecord>()
            //    .HasOne(r => r.OrderStatus)
            //    .WithMany(s => s.RecyclingRecords)
            //    .HasForeignKey(r => r.OrderStatusId);

            // One-to-Many: RecyclingRecords -> Location
            modelBuilder.Entity<RecyclingRecord>()
                .HasOne(r => r.Location)
                .WithMany(s => s.RecyclingRecords)
                .HasForeignKey(r => r.LocationId);

            modelBuilder.Entity<ImageStorage>()
                .HasOne(i => i.User)
                .WithMany(u => u.ImagesStorage)  
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RecyclingRecord>()
                .HasOne(i => i.ImagesStorage)
                .WithOne(r => r.RecyclingRecord)               
                .HasForeignKey<RecyclingRecord>(i => i.ImageId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
