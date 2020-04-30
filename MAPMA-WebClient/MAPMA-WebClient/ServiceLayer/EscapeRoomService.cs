using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAPMA_WebClient.EscRef;

namespace MAPMA_WebClient.ServiceLayer {

    public class EscapeRoomService {

        public EscapeRoomService() {
        
        }

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

        public List<EscapeRoom> GetAllEscapeRooms() {
            IEscapeRoom_Services escServ = new EscapeRoom_ServicesClient();

            return escServ.GetAllForOwner();
        }

        public List<TimeSpan> FreeTimes(int ER_ID, DateTime Bdate) {
            IEscapeRoom_Services escServ = new EscapeRoom_ServicesClient();
            Console.WriteLine(escServ.FreeTimes(ER_ID, Bdate).Count);
           
            return escServ.FreeTimes(ER_ID,Bdate);
        }

    }
}    
