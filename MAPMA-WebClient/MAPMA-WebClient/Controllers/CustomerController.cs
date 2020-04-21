using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAPMA_WebClient.CusRef;

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
            ICustomerServices cs = new CustomerServicesClient();
            Customer cus = cs.Get(Username); 
            return View(cus);
        }
        
    }
}