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


        public ActionResult FormulaRegister() {
            return View();
        }

        [HttpPost]
        public ActionResult Register ( string Firstname, string Lastname, string Mail, string Phone, string Username, string Password )
        {
            CustomerService cs = new CustomerService();
            Customer cus = new Customer()
            {
                firstName = Firstname,
                lastName = Lastname,
                mail = Mail,
                phone = Phone,
                username = Username
            };
            int i = cs.Register(cus, Password);
            if (i == 1) { 
            
                return RedirectToAction("Login", "Customer");
            }
            else if (i== 0) {
                TempData["message1"] = "dette brugernavn er i brug";
                return RedirectToAction("FormulaRegister", "Customer");
            }
            else {
                TempData["message2"] = "Der gik noget galt, prøv igen. Aberne er i gang med at løse det";
                return RedirectToAction("FormulaRegister", "Customer");
                
            }
            
        }

        public ActionResult Login ( )
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginComplet(string inputPassword, string username )
        {
            CustomerService cs = new CustomerService();
            Customer cus = new Customer();
            cs.Login(inputPassword, username);
            return RedirectToAction("GetAllEscapeRoom", "EscapeRoom");
        }




    }
}