using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tech.Assessment.API.DTO.Response
{
    public class AddressDTO
    {

        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public string FullAddress
        {
            get
            {

                return string.Format("{0}, {1}, {2}", this.Street, this.City, this.PostalCode);

            }
        }

    }
}
