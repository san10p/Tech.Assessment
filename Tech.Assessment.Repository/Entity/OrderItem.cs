using System;
using System.Collections.Generic;
using System.Text;

namespace Tech.Assessment.Repository.Entity
{
  public  class OrderItem : BaseEntity<int>
    {
        public int OrderId { get; set; }
        public int ProductType { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; }
    }
}
