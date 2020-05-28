using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAPMA_WebClient.CusRef;

namespace MAPMA_WebClient.ServiceLayer {
    /// <summary>
    /// <author>
    /// Mick O. B. Andersen
    /// Anders S. Brygger
    /// Peter S. Clausen
    /// Anders B. Larsen
    /// Mads G. Ranzau
    /// </author>
    /// </summary>
    public class CustomerService {

        /// <summary>
        /// Constructor for CustomerService
        /// </summary>
        public CustomerService() {
        
        }

        /// <summary>
        /// Gets a Customer
        /// </summary>
        /// <param name="username">A Username from the user</param>
        /// <returns>A Customer</returns>
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

        /// <summary>
        /// Register a customer in the database
        /// </summary>
        /// <param name="cus"> A customer with all fields filled</param>
        /// <param name="password"> A chosen password</param>
        /// <returns>return 0 if failed, returns 1 if succesful and returns 2 if an expcetion happend</returns>
        public int Register(Customer cus, string password) {
            ICustomerServices CusServ = new CustomerServicesClient();
           return CusServ.Register(cus, password);
        }

        /// <summary>
        /// Login a Customer
        /// </summary>
        /// <param name="inputPassword">The password that was input</param>
        /// <param name="username">The username that was input</param>
        /// <returns>A customer if successfull and returns null if failed</returns>
        public Customer Login(string inputPassword, string username )
        {
            ICustomerServices CusServ = new CustomerServicesClient();
            return CusServ.Login(inputPassword, username);

        }

    }
}
