using MAPMA_WebClient.EscRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

            IEscapeRoom_Services ecss = new EscapeRoom_ServicesClient();
            EscapeRoom escr = ecss.GetForOwner(id);
            return View(escr);
        }

        public ActionResult GetAllEscapeRoom ( )
        {
            IEscapeRoom_Services ecss = new EscapeRoom_ServicesClient();

            //EscapeRoom[] escapeRooms;
            List<EscapeRoom> escapeRooms = ecss.GetAllForOwner();
            ViewBag.List = escapeRooms;
            return View();
        }

    }
}