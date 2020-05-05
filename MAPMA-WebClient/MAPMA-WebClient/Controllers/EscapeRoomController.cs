using MAPMA_WebClient.EscRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAPMA_WebClient.ServiceLayer;
using System.Drawing;
using System.IO;

namespace MAPMA_WebClient.Controllers
{
    public class EscapeRoomController : Controller    {

        
        public ActionResult GetEscapeRoom ( int id )        {

            EscapeRoomService ecss = new EscapeRoomService();
            EscRef.EscapeRoom escr = ecss.GetEscapeRoom(id);
            ViewBag.Escaperoom1 = escr;
            return View();
        }

        public ActionResult GetAllEscapeRoom ( )
        {
            EscapeRoomService ecss = new EscapeRoomService();

            //EscapeRoom[] escapeRooms;
            List<EscapeRoom> escapeRooms = ecss.GetAllEscapeRooms();
            ViewBag.List = escapeRooms;
            return View();
        }

        public Image PicShow(byte[] imgArray) {

            return ByteArrayToImage(imgArray);
        }

        public Image ByteArrayToImage ( byte[] byteArrayIn )
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                var returnImage = Image.FromStream(ms);

                return returnImage;
            }
        }


    }
}