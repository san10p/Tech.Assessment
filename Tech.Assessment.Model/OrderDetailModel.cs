using System;
using System.Collections.Generic;
using Tech.Assessment.Model.Enum;

namespace Tech.Assessment.Model
{
    public class OrderDetailModel: BaseModel<int>
    {
        public string OrderID { get; set; }
        public List<OrderItemModel> Items { get; set; }
        public DateTime DeliveryOn { get; set; }
        public int CustomerId { get; set; }
        public double RequiredBinWidth { get; set; }
        public CustomerModel Customer { get; set; }
    }
}
