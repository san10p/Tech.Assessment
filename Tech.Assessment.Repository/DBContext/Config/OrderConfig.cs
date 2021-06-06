using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Assessment.Repository.Entity;

namespace Tech.Assessment.Repository.DBContext.Config
{
    public class OrderConfig :  IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(d => d.Customer)
                            .WithMany(p => p.Orders)
                            .HasForeignKey(d => d.CustomerId);          


        }
    }
}
