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
    /// <summary>
    /// <author>
    /// Mick O. B. Andersen
    /// Anders S. Brygger
    /// Peter S. Clausen
    /// Anders B. Larsen
    /// Mads G. Ranzau
    /// </author>
    /// </summary>
    public class EscapeRoomController : Controller    {

        /// <summary>
        ///  Gets an escaperoom
        /// </summary>
        /// <param name="id">The EscapeRoom ID</param>
        /// <returns>returns the View GetEscapeRoom</returns>
        public ActionResult GetEscapeRoom ( int id )        {

            EscapeRoomService ecss = new EscapeRoomService();
            EscRef.EscapeRoom escr = ecss.GetEscapeRoom(id);
            ViewBag.Escaperoom1 = escr;
            return View();
        }

        /// <summary>
        /// Gets a list of esacperooms
        /// </summary>
        /// <returns>Returns the view GetAllEscapeRoom</returns>
        public ActionResult GetAllEscapeRoom ( )
        {
            EscapeRoomService ecss = new EscapeRoomService();

            //EscapeRoom[] escapeRooms;
            List<EscapeRoom> escapeRooms = ecss.GetAllEscapeRooms();
            ViewBag.List = escapeRooms;
            return View();
        }

        /// <summary>
        /// Shows the picture from an escaperoom
        /// </summary>
        /// <param name="imgArray">the bytes of an img</param>
        /// <returns>Returns an image via the method ByteArrayToImage</returns>
        public Image PicShow(byte[] imgArray) {

            return ByteArrayToImage(imgArray);
        }

        /// <summary>
        /// Converts the bytes to an image
        /// </summary>
        /// <param name="byteArrayIn">the bytes of an img</param>
        /// <returns>Returns the converted image</returns>
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