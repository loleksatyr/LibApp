using LibApp.Data;
using LibApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Services
{
    public interface ICustomersServices {
        Customer GetCustomerDetails(int id);
        Customer GetCustomers(int id);
        IEnumerable<MembershipType> GetMemberships();
        Customer InsertCustomer(Customer customer);
        Customer EditCustomer(Customer customer);

    }
    public class CustomersServices : ICustomersServices
    {

        ApplicationDbContext _context;
        public CustomersServices(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public Customer EditCustomer(Customer customer)
        {
            var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
            customerInDb.Name = customer.Name;
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            customerInDb.HasNewsletterSubscribed = customer.HasNewsletterSubscribed;
            _context.SaveChanges();
            return customerInDb;
        }


        public Customer GetCustomerDetails(int id)
        {
            return _context.Customers
                .Include(c => c.MembershipType)
                .SingleOrDefault(c => c.Id == id);
        }

        public Customer GetCustomers(int id)
        {
            return _context.Customers.SingleOrDefault(c => c.Id == id);
        }


        public IEnumerable<MembershipType> GetMemberships()
        {
            return _context.MembershipTypes.ToList();
        }

        public Customer InsertCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }
    }
}
