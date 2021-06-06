using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tech.Assessment.API.ValidateAttribute;
using Tech.Assessment.Model.Enum;

namespace Tech.Assessment.API.DTO.Request.Order
{   
    public class OrderDTO
    {
        [Required]
        public string OrderID { get; set; }
        public List<OrderItemDTO> Items { get; set; }
        public DateTime DeliveryOn { get; set; }
        [Required]
        public int CustomerId { get; set; }
    }
    public class OrderItemDTO
    {
        [Required]
        [ValidateEnumAttribute(typeof(ProductType), ErrorMessage = "InValid Product Type")]
        public ProductType ProductType { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
}
