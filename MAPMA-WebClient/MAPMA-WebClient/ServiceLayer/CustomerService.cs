using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAPMA_WebClient.CusRef;

namespace MAPMA_WebClient.ServiceLayer {
    public class CustomerService {

        public CustomerService() {
        
        }
       
        public Customer GetCustomer(string username) {
        ICustomerServices CusServ = new CustomerServicesClient();
        try {
            return CusServ.Get(username);
        }
        catch (NullReferenceException NE) {
            Console.WriteLine(NE);
            return null;
        }

            
    }
        public int Register(Customer cus, string password) {
            ICustomerServices CusServ = new CustomerServicesClient();
           return CusServ.Register(cus, password);
        }

        public Customer Login(string inputPassword, string username )
        {
            ICustomerServices CusServ = new CustomerServicesClient();
            return CusServ.Login(inputPassword, username);
        }

    }
}
