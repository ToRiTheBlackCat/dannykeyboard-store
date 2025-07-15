using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.DTOs.Customer
{
    public class ListCustomerResponseDto
    {
        public int CustomerCount { get; set; }
        public List<Domain.Entities.Customer> CustomerList { get; set; } = new List<Domain.Entities.Customer>();
    }
}
