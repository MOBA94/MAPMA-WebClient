using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAPMA_WebClient.CusRef;
using MAPMA_WebClient.ServiceLayer;

namespace MAPMA_WebClient.Controllers
{
    public class CustomerController : Controller {

        public ActionResult GetCustomer ( )
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetCustomer(string Username) 
        {
            CustomerService cs = new CustomerService();
            Customer cus = cs.GetCustomer(Username); 
            return View(cus);
        }
        
    }
}