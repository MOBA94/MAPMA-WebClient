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
            Console.ReadLine();
            return null;
        }
    }


}
}
