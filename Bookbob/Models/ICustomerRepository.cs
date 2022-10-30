using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookbob.Models
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(long Id);
        IEnumerable<Customer> GetAllCustomers();
        Customer Add(Customer Student);
        Customer Update(Customer StudentChanges);
        Customer Delete(long Id);
    }
}
