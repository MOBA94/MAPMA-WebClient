using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAPMA_WebClient.CusRef;
using MAPMA_WebClient.ServiceLayer;
using MAPMA_WebClient.BookRef;

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
            CusRef.Customer cus = cs.GetCustomer(Username); 
            return View(cus);
        }

        
        public ActionResult FormulaRegister() {
            return View();
        }

        [HttpPost]
        public ActionResult Register ( string Firstname, string Lastname, string Mail, string Phone, string Username, string Password )
        {
            CustomerService cs = new CustomerService();
            CusRef.Customer cus = new CusRef.Customer()
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
                TempData["message1"] = "Dette brugernavn er i brug";
                return RedirectToAction("FormulaRegister", "Customer");
            }
            else {
                TempData["message2"] = "Der gik noget galt, prøv igen. Aberne er i gang med at løse det";
                return RedirectToAction("FormulaRegister", "Customer");
                
            }
            
        }
        
        public ActionResult GetAllBookingFromUser(string username) {
            try {
                HttpCookieCollection myCookieCollection = Request.Cookies;
                HttpCookie mycookie = myCookieCollection.Get("user");
                if (mycookie != null) {
                    BookingService bs = new BookingService();
                    List<Booking> userbooking = bs.GetAllBookingFromUser(username);
                    ViewBag.Userbook = userbooking;

                    return View();
                }
                else {
                    TempData["message"] = "du har prøvet at gå til en side hvor man skal være logget ind først. lave en bruger eller log ind.";
                    return RedirectToAction("Login", "Customer");
                }
            }
            catch (NullReferenceException e) {
                TempData["message"] = "du har prøvet at gå til en side hvor man skal være logget ind først. lave en bruger eller log ind.";
                return RedirectToAction("Login", "Customer");
            }
        }

        public ActionResult Login ( )
        {
            return View();
        }

        [HttpPost]        
        [AllowAnonymous]        
        public ActionResult LoginComplet(string inputPassword, string username )
        {
            try {
                CustomerService cs = new CustomerService();
                CusRef.Customer cus = new CusRef.Customer();
                cus = cs.Login(inputPassword, username);
                if (cus != null) {
                    Session["user"] = cus.username;
                    HttpCookie userCookie = new HttpCookie("user", cus.username);
                    userCookie.Expires.AddHours(2);
                    HttpContext.Response.SetCookie(userCookie);

                    return RedirectToAction("GetAllBookingFromUser", "Customer", new {
                        username = @Request.Cookies["User"].Value
                    });
                }
                else {
                    TempData["message"] = "det skete en fejl";
                    return RedirectToAction("Login", "Customer");
                }
            }
            catch(ArgumentException e) {
                
                TempData["message1"] = "det skete en fejl";
                return RedirectToAction("Login", "Customer");
            }
        }

        public ActionResult Logout() {
            Session.Abandon();
            if (Request.Cookies["user"] != null) {
                var c = new HttpCookie("user");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            return RedirectToAction("Login", "Customer");
        }




    }
}