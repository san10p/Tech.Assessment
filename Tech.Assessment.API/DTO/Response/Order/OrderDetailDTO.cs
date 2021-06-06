
using Tech.Assessment.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tech.Assessment.API.DTO.Response.Customer;

namespace Tech.Assessment.API.DTO.Response.Order
{
    public class OrderDetailDTO
    {
        public string OrderID { get; set; }
        public List<ItemDetailDTO> Items { get; set; }      
        public double RequiredBinWidth { get; set; }
        public DateTime DeliveryOn { get; set; }
        public DateTime CreatedAt { get; set; }
        public CustomerDTO Customer { get; set; }
    }
    public class ItemDetailDTO
    {
        public int ItemId { get; set; }
        public ProductType ProductType { get; set; }
        public int Quantity { get; set; }

    }
}
