using MAPMA_WebClient.BookRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAPMA_WebClient.ServiceLayer;

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
    public class BookingController : Controller 
    {

        /// <summary>
        /// Starts the booking by getting the escaperoom with the id
        /// </summary>
        /// <param name="id">The EscapeRoom ID</param>
        /// <returns>Returns the view CreateBooking if customer is logged in and redirects to login if not</returns>
        public ActionResult CreateBooking(int id) {
            EscapeRoomService escs = new EscapeRoomService();
            EscRef.EscapeRoom es = escs.GetEscapeRoom(id);
            ViewBag.TimeList = es.AvalibleTimes;
            ViewBag.EscapeRoom = es;
            HttpCookie cookie = Request.Cookies["User"];

        if(cookie == null) {
                return RedirectToAction("Login", "Customer");
            }

            return View();
        }

        /// <summary>
        /// Countiues the creation of a booking and gives it a date
        /// </summary>
        /// <param name="id">The EscapeRoom ID</param>
        /// <param name="BDate">The Date of the booking</param>
        /// <returns>Returns the view CreateBookingCheck or redirects to CreateBooking if BDate == null and if no freetimes are avalible</returns>
        [HttpPost]
        public ActionResult CreateBookingCheck(int id, DateTime BDate) {
            EscapeRoomService escs = new EscapeRoomService();
            EscRef.EscapeRoom es = escs.GetEscapeRoom(id);
            ViewBag.EscapeRoom = es;
            List<TimeSpan> freetime = escs.FreeTimes(id, BDate);
            if (BDate == null)
            {
                TempData["message"] = "Der er ikke valgt en dato";
                return RedirectToAction("CreateBooking", new { id = id });
            }
            else if (freetime.Count == 0)
            {
                TempData["message"] = "Der ingen tilgængelige booking tider";
                return RedirectToAction("CreateBooking", new { id = id });
            }
            else
            {
                ViewBag.freetimes = freetime;
                ViewBag.ChosenDate = BDate.ToString("dd-MM-yyyy");
            }

            return View();
        }

        /// <summary>
        /// Finishes the booking and puts it in the database
        /// </summary>
        /// <param name="EmployeeID">An employees ID</param>
        /// <param name="username">A Username from the user</param>
        /// <param name="escapeRoomID">The EscapeRoom ID</param>
        /// <param name="BookTime">The Time for the booking</param>
        /// <param name="AmountOfPeople">The Amount of people</param>
        /// <param name="BDate">The Date of the booking</param>
        /// <returns>Returns the View CreateBookingDone</returns>
        [HttpPost]
        public ActionResult CreateBookingDone(int EmployeeID, string username, int escapeRoomID, TimeSpan BookTime, int AmountOfPeople, DateTime BDate)
        {
            EscapeRoomService escs = new EscapeRoomService();
            EscRef.EscapeRoom es = escs.GetEscapeRoom(escapeRoomID);
            ViewBag.EscapeRoom = es;
            List<TimeSpan> freetime = escs.FreeTimes(escapeRoomID, BDate);
            ViewBag.freetimes = freetime;
            BookingService bs = new BookingService();
            HttpCookie cookie = Request.Cookies["User"];
            cookie.Value = username;

            int c = bs.CreateBooking(EmployeeID, username, escapeRoomID, BookTime, AmountOfPeople, BDate);
            if (c == 0) {
                TempData["message"] = "Den tid du prøvede at booke er desværre blevet taget ";
                return RedirectToAction("CreateBooking", new { id = escapeRoomID });
            }
            return View();
        }

        /// <summary>
        /// Gets a booking from the database
        /// </summary>
        /// <param name="escapeRoomID">The EscapeRoom ID</param>
        /// <param name="username">A Username from the user</param>
        /// <param name="BDate">The Date of the booking</param>
        /// <returns>Returns the view GetBooking with the found booking</returns>
        [HttpGet]
        public ActionResult GetBooking (int escapeRoomID, string username, DateTime BDate)
        {
            BookingService bs = new BookingService();
            Booking book = bs.GetBooking(escapeRoomID, username, BDate);
            return View(book);
        }

        /// <summary>
        /// Deletes a Booking form the database
        /// </summary>
        /// <param name="EmployeeID">An employees ID</param>
        /// <param name="username">A Username from the user</param>
        /// <param name="escapeRoomID">The EscapeRoom ID</param>
        /// <param name="BookTime"> The Time for the booking</param>
        /// <param name="AmountOfPeople">The Amount of people</param>
        /// <param name="BDate">The Date of the booking</param>
        /// <returns>Returns the view DeleteBooking</returns>
        [HttpDelete]
        public ActionResult DeleteBooking (int EmployeeID, string username, int escapeRoomID, TimeSpan BookTime, int AmountOfPeople, DateTime BDate)
        {
            BookingService bs = new BookingService();
            bs.DeleteBooking(EmployeeID, username, escapeRoomID, BookTime, AmountOfPeople, BDate);
            return View();
        }

        /// <summary>
        /// Deletes a booking from the customers list of bookings
        /// </summary>
        /// <param name="username">A Username from the user</param>
        /// <param name="escapeRoomID">The EscapeRoom ID</param>
        /// <param name="BookTime"> The Time for the booking</param>
        /// <param name="BDate">The Date of the booking</param>
        /// <returns>Returns a redirects to GetAllBookingFromUser with the deleted booking no longer on the list. If an exception happens redirect to Login with a message</returns>
        [HttpDelete]
        public ActionResult DeleteBookingCustomer(string username, int escapeRoomID, TimeSpan BookTime, DateTime BDate) {
            try {                
                BookingService bs = new BookingService();
                bs.DeleteBookingCustomer(username, escapeRoomID, BookTime, BDate);               

                return RedirectToAction("GetAllBookingFromUser", "Customer", new { username = @Request.Cookies["User"].Value });
            }
            catch(NullReferenceException e) {
                TempData["message"] = "du har prøvet at gå til en side hvor man skal være logget ind først. lave en bruger eller log ind.";
                return RedirectToAction("Login", "Customer");
            }
        }

        /// <summary>
        /// Get all the bookings from the database
        /// </summary>
        /// <returns>Returns the view GetAllBookings</returns>
        public ActionResult GetAllBookings() {
            BookingService bs = new BookingService();
           List<Booking> books =  bs.GetAllBookings();
            ViewBag.List = books;
            return View();
        }

    }
}