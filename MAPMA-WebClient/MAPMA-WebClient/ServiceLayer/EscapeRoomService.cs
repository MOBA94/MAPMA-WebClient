using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAPMA_WebClient.EscRef;

namespace MAPMA_WebClient.ServiceLayer {

    /// <summary>
    /// <author>
    /// Mick O. B. Andersen
    /// Anders S. Brygger
    /// Peter S. Clausen
    /// Anders B. Larsen
    /// Mads G. Ranzau
    /// </author>
    /// </summary>
    public class EscapeRoomService {

        /// <summary>
        /// Constructor for EscapeRoomService
        /// </summary>
        public EscapeRoomService() {
        
        }

        /// <summary>
        /// Gets an EscapeRoom
        /// </summary>
        /// <param name="escapeRoomID">The EscapeRoom ID</param>
        /// <returns>returns an escaperoom</returns>
        public EscapeRoom GetEscapeRoom(int escapeRoomID) {
            IEscapeRoom_Services escServ = new EscapeRoom_ServicesClient();
            try {
                return escServ.GetForOwner(escapeRoomID);
            }
            catch (NullReferenceException NE) {
                Console.WriteLine(NE);
                Console.ReadLine();
                return null;
            }
         }

        /// <summary>
        /// Gets all the escapeRooms
        /// </summary>
        /// <returns>returns the list of all the escapeRooms</returns>
        public List<EscapeRoom> GetAllEscapeRooms() {
            IEscapeRoom_Services escServ = new EscapeRoom_ServicesClient();

            return escServ.GetAllForOwner();
        }

        /// <summary>
        /// Gets the freetimes avalible for the chosen escaperoom
        /// </summary>
        /// <param name="ER_ID">The EscapeRoom ID</param>
        /// <param name="Bdate">The Date of the booking</param>
        /// <returns>returns a list of the times avalible for the escaperoom</returns>
        public List<TimeSpan> FreeTimes(int ER_ID, DateTime Bdate) {
            IEscapeRoom_Services escServ = new EscapeRoom_ServicesClient();
            Console.WriteLine(escServ.FreeTimes(ER_ID, Bdate).Count);
           
            return escServ.FreeTimes(ER_ID,Bdate);
        }

    }
}    
