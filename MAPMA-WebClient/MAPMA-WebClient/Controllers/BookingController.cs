using MAPMA_WebClient.BookRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAPMA_WebClient.Controllers
{
    public class BookingController : Controller 
    { 
        public ActionResult CreateBooking( )
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBooking (int EmployeeID, string username, int escapeRoomID, DateTime BookTime, int AmountOfPeople, DateTime BDate)
        {
            IBookingServices bs = new BookingServicesClient();
            bs.Create(EmployeeID, username, escapeRoomID, BookTime, AmountOfPeople, BDate);
            return View();
        }

    }
}