using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tech.Assessment.API.DTO.Response.Customer
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public AddressDTO Address { get; set; }
    }
}
