using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookbob.Models
{
    public class SQLCustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext context;
        public SQLCustomerRepository(AppDbContext context)
        {
            this.context = context;
        }
        Customer ICustomerRepository.Add(Customer Customer)
        {
            context.Customers.Add(Customer);
            context.SaveChanges();
            return Customer;
        }
        Customer ICustomerRepository.Delete(long Id)
        {
            Customer Customer = context.Customers.Find(Id);
            if (Customer != null)
            {
                context.Customers.Remove(Customer);
                context.SaveChanges();
            }
            return Customer;
        }

        IEnumerable<Customer> ICustomerRepository.GetAllCustomers()
        {
            return context.Customers;
        }

        Customer ICustomerRepository.GetCustomer(long id)
        {
            return context.Customers.FirstOrDefault(m => m.Id == id);
        }

        Customer ICustomerRepository.Update(Customer CustomerChanges)
        {
            var Customer = context.Customers.Attach(CustomerChanges);
            Customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return CustomerChanges;
        }
    }
}
