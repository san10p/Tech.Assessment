using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech.Assessment.Repository.Entity
{
    public class Customer : BaseEntity<int>
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
