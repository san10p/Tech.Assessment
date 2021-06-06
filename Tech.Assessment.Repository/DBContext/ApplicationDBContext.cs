
using Tech.Assessment.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Tech.Assessment.Repository.DBContext.Config;

namespace Tech.Assessment.Repository.DBContext
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
        {
        }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
        {
        }

        public DbSet<PackageCalculationDetail> PackageCalculationDetail { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            EntityRelationships(modelBuilder);
        }

        private void EntityRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfig());
            modelBuilder.Entity<Order>()
            .HasIndex(p =>  p.OrderID )
            .IsUnique(true);
        }
    }
}
