using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MeetingsAPI.Entities
{
    public class MeetingsDbContext : DbContext
    {
        string _connectionString = "Server=DESKTOP-R8L9JN2\\LEARNINGSQL;Database=MeetingDb;Trusted_Connection=True;";

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50);

            modelBuilder.Entity<User>()
            .Property(r => r.Email)
            .IsRequired()
            .HasMaxLength(50);

            modelBuilder.Entity<Meeting>()
            .Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(200);
            modelBuilder.Entity<Meeting>()
            .Property(r => r.Time)
            .IsRequired();

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
