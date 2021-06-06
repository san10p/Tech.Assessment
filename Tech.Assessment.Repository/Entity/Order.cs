using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tech.Assessment.Repository.Entity
{
    public class Order : BaseEntity<int>
    {
        public Order()
        {
            Items = new HashSet<OrderItem>();
        }
        [Key]
        public string OrderID { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public DateTime DeliveryOn { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
