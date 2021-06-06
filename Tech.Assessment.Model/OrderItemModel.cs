using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Assessment.Model.Enum;

namespace Tech.Assessment.Model
{
  public  class OrderItemModel
    {
        public int OrderId { get; set; }
        public ProductType ProductType { get; set; }
        public int Quantity { get; set; }
      
    }
}
