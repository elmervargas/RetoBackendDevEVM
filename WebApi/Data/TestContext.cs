using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data
{
    public class TestContext : DbContext
    {
        public DbSet<Address> Address { get; set; }

        public DbSet<Email> Email { get; set; }

        public DbSet<Phone> Phone { get; set; }

        public DbSet<Person> Person { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=MYSQL5044.site4now.net;Database=db_a6dcf6_backevm;Uid=a6dcf6_backevm;Pwd=P@ssw0rd");
            //optionsBuilder.UseMySQL("server=localhost;port=3306;database=dbtest3;user=root;password=P@ssw0rd");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Firstname).IsRequired();
                entity.Property(e => e.Lastname).IsRequired();
                entity.Property(e => e.Birthdate).IsRequired();
                entity.Property(e => e.Documenttype).IsRequired();
                entity.Property(e => e.Documentnumber).IsRequired();
                entity.Property(e => e.Photo).HasColumnType("longblob");
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired();
                entity.HasOne(d => d.Person).WithMany(p => p.Addresses)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired();
                entity.HasOne(d => d.Person).WithMany(p => p.Emails)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired();
                entity.HasOne(d => d.Person).WithMany(p => p.Phones)
                        .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
