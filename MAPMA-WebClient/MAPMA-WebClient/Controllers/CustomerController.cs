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
    /// <summary>
    /// <author>
    /// Mick O. B. Andersen
    /// Anders S. Brygger
    /// Peter S. Clausen
    /// Anders B. Larsen
    /// Mads G. Ranzau
    /// </author>
    /// </summary>
    public class CustomerController : Controller {

        /// <summary>
        /// An empty version of the GetCustomer method
        /// </summary>
        /// <returns>returns the view GetCustomer with nothing</returns>
        public ActionResult GetCustomer ( )
        {
            return View();
        }

        /// <summary>
        /// Gets a customer with the parameters
        /// </summary>
        /// <param name="Username">A Username from the user</param>
        /// <returns>returns the view GetCustomer with the chosen users info</returns>
        [HttpPost]
        public ActionResult GetCustomer(string Username) 
        {
            CustomerService cs = new CustomerService();
            CusRef.Customer cus = cs.GetCustomer(Username); 
            return View(cus);
        }

        /// <summary>
        /// Gets the view of the register page
        /// </summary>
        /// <returns>returns the view FormulaRegister</returns>
        public ActionResult FormulaRegister() {
            return View();
        }

        /// <summary>
        /// Registers a customer to the database
        /// </summary>
        /// <param name="Firstname">the first name of the customer</param>
        /// <param name="Lastname">the last name of the customer</param>
        /// <param name="Mail">the mail of the customer</param>
        /// <param name="Phone">the phone number of the customer</param>
        /// <param name="Username">the input username</param>
        /// <param name="Password">the input password</param>
        /// <returns>returns a Redirects to either Login if succesfull or the FormulaRegister with a message if failed</returns>
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

        /// <summary>
        /// Gets all the bookings from the user
        /// </summary>
        /// <param name="username">A Username of the user</param>
        /// <returns>returns the view GetAllBookingFromUser with the chosen user if failed redirects to Login with a message</returns>
        public ActionResult GetAllBookingFromUser() {
            try {
                HttpCookieCollection myCookieCollection = Request.Cookies;
                HttpCookie mycookie = myCookieCollection.Get("user");
                if (mycookie != null) {
                        BookingService bs = new BookingService();
                        List<Booking> userbooking = bs.GetAllBookingFromUser(mycookie.Value);
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

        /// <summary>
        /// A way to get the login page
        /// </summary>
        /// <returns>Returns the view Login</returns>
        public ActionResult Login ( )
        {
            return View();
        }

        /// <summary>
        /// Completes the Login by cheacking the customer is in the database
        /// </summary>
        /// <param name="inputPassword">the input password</param>
        /// <param name="username">the input username</param>
        /// <returns>retunrs a redirect to GetAllBookingFromUser if succesfull or redirects to Login with a message if failed</returns>
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

                    return RedirectToAction("GetAllBookingFromUser", "Customer");
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

        /// <summary>
        /// Logs out the customer
        /// </summary>
        /// <returns>Returns a redirect to Login</returns>
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