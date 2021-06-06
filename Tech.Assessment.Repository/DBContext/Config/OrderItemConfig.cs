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
    public class OrderItemConfig :  IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasOne(d => d.Order)
                            .WithMany(p => p.Items)
                            .HasForeignKey(d => d.OrderId);          


        }
    }
}
