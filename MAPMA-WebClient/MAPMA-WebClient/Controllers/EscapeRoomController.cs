using MAPMA_WebClient.EscRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAPMA_WebClient.ServiceLayer;

namespace MAPMA_WebClient.Controllers
{
    public class EscapeRoomController : Controller
    {
        public ActionResult GetEscapeRoom ( )
        {

            return View();
        }

        [HttpPost]
        public ActionResult GetEscapeRoom ( int id )
        {

            EscapeRoomService ecss = new EscapeRoomService();
            EscapeRoom escr = ecss.GetEscapeRoom(id);
            return View(escr);
        }

        public ActionResult GetAllEscapeRoom ( )
        {
            EscapeRoomService ecss = new EscapeRoomService();

            //EscapeRoom[] escapeRooms;
            List<EscapeRoom> escapeRooms = ecss.GetAllEscapeRooms();
            ViewBag.List = escapeRooms;
            return View();
        }

    }
}