using CustomerAppEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerAppBLL
{
 
    public interface ICustomerService
    {
      
        Customer Create(Customer cust);
     
        List<Customer> GetAll();
        Customer Get(int Id);
      
        Customer Update(Customer cust);
      
        Customer Delete(int Id);
    }
}
