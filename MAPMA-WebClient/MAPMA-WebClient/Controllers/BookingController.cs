using MAPMA_WebClient.BookRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAPMA_WebClient.ServiceLayer;

namespace MAPMA_WebClient.Controllers
{
    public class BookingController : Controller 
    {
        public ActionResult CreateBooking(int id) {
            EscapeRoomService escs = new EscapeRoomService();
            EscRef.EscapeRoom es = escs.GetEscapeRoom(id);
            ViewBag.TimeList = es.AvalibleTimes;
            ViewBag.EscapeRoom = es;

            return View();
        }

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
                ViewBag.ChosenDate = BDate;
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateBookingDone(int EmployeeID, string username, int escapeRoomID, TimeSpan BookTime, int AmountOfPeople, DateTime BDate)
        {
            EscapeRoomService escs = new EscapeRoomService();
            EscRef.EscapeRoom es = escs.GetEscapeRoom(escapeRoomID);
            ViewBag.EscapeRoom = es;
            List<TimeSpan> freetime = escs.FreeTimes(escapeRoomID, BDate);
            ViewBag.freetimes = freetime;
            BookingService bs = new BookingService();
            if(bs == 0)
            bs.CreateBooking(EmployeeID, username, escapeRoomID, BookTime, AmountOfPeople, BDate);
            return View();
        }

        [HttpPost]
        public ActionResult GetBooking (int escapeRoomID, string username, DateTime BDate)
        {
            BookingService bs = new BookingService();
            Booking book = bs.GetBooking(escapeRoomID, username, BDate);
            return View(book);
        }

        [HttpPost]
        public ActionResult DeleteBooking (int EmployeeID, string username, int escapeRoomID, TimeSpan BookTime, int AmountOfPeople, DateTime BDate)
        {
            BookingService bs = new BookingService();
            bs.DeleteBooking(EmployeeID, username, escapeRoomID, BookTime, AmountOfPeople, BDate);
            return View();
        }

        public ActionResult GetAllBookings() {
            BookingService bs = new BookingService();
           List<Booking> books =  bs.GetAllBookings();
            ViewBag.List = books;
            return View();
        }

    }
}