using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAPMA_WebClient.BookRef;

namespace MAPMA_WebClient.ServiceLayer
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
    public class BookingService
    {
        /// <summary>
        /// Constructor for BookingService
        /// </summary>
        public BookingService() {
        
        }

        /// <summary>
        /// Creates Booking
        /// </summary>
        /// <param name="EmployeeID"> An employees ID</param>
        /// <param name="username"> A Username from the user</param>
        /// <param name="escapeRoomID">The EscapeRoom ID</param>
        /// <param name="BookTime"> The Time for the booking</param>
        /// <param name="AmountOfPeople">The Amount of people</param>
        /// <param name="BDate">The Date of the booking</param>
        /// <returns> returns 1 if succesful and returns 0 if failed.</returns>
        public int CreateBooking(int EmployeeID, string username, int escapeRoomID, TimeSpan BookTime, int AmountOfPeople, DateTime BDate) {
            IBookingServices Service = new BookingServicesClient();

            return Service.Create(EmployeeID, username, escapeRoomID, BookTime, AmountOfPeople, BDate);
        }

        /// <summary>
        /// Deletes a Booking from the database.
        /// </summary>
        /// <param name="EmployeeID"> An employees ID </param>
        /// <param name="username"> A Username from the user </param>
        /// <param name="escapeRoomID"> The EscapeRoom ID </param>
        /// <param name="BookTime">The Time for the booking </param>
        /// <param name="AmountOfPeople"> The Amount of people</param>
        /// <param name="BDate"> The Date of the booking</param>
        public void DeleteBooking(int EmployeeID, string username, int escapeRoomID, TimeSpan BookTime, int AmountOfPeople, DateTime BDate) {
            IBookingServices Service = new BookingServicesClient();          

            Service.Delete(EmployeeID, username, escapeRoomID, BookTime, AmountOfPeople, BDate); 
        }

        /// <summary>
        /// Deletes a Booking from the database.
        /// </summary>
        /// <param name="username"> A Username from the user</param>
        /// <param name="escapeRoomID">The EscapeRoom ID</param>
        /// <param name="BookTime">The Time for the booking</param>
        /// <param name="BDate">The Date of the booking</param>
        public void DeleteBookingCustomer(string username, int escapeRoomID, TimeSpan BookTime, DateTime BDate) {
            IBookingServices Service = new BookingServicesClient();

            Service.Deleteweb(username, escapeRoomID, BookTime, BDate);
        }

        /// <summary>
        /// Gets a booking from the database.
        /// </summary>
        /// <param name="escapeRoomID">The EscapeRoom ID</param>
        /// <param name="username"> A Username from the user</param>
        /// <param name="BDate">The Date of the booking</param>
        /// <returns>The Booking</returns>
        public Booking GetBooking(int escapeRoomID, string username, DateTime BDate) {
            IBookingServices Service = new BookingServicesClient();

            try {
                Booking Book = Service.Get(escapeRoomID, username, BDate);
                return Book;
            }
            catch (NullReferenceException NE) {
                Console.WriteLine(NE);
                Console.ReadLine();
                return null;
            }
        }

        /// <summary>
        /// Gets all bookings from the database.
        /// </summary>
        /// <returns>List of all bookings</returns>
        public List<Booking> GetAllBookings() {
            IBookingServices Service = new BookingServicesClient();

            return Service.GetAll();

        }

        /// <summary>
        /// Gets all the users bookings.
        /// </summary>
        /// <param name="username"> A Username from the user</param>
        /// <returns>A List of all the customers booking</returns>
        public List<Booking> GetAllBookingFromUser(string username) {
            IBookingServices Service = new BookingServicesClient();
            return Service.GetAllFromUser(username);
        }
    }
}