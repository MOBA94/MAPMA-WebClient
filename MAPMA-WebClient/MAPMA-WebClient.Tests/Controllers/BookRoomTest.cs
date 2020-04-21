using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MAPMA_WebClient.Controllers;
using MAPMA_WebClient.BookRef;

namespace MAPMA_WebClient.Tests.Controllers
{
    [TestClass]
    public class BookRoomTest
    {
        [TestMethod]
        public void CreateNewBookingTest ( )
        {
            //Arrange
            BookingController bc = new BookingController();
            CustomerController cc = new CustomerController();
            EscapeRoomController ec = new EscapeRoomController();
            EmployeeController emc = new EmployeeController();
            
           var cus = new Customer();           
            cus.username = "Anorak";
            Employee em = new Employee();
            em.employeeID = 1;
            EscapeRoom er = new EscapeRoom();
            er.escapeRoomID = 1;
            Booking hostBook;
            Booking book = new Booking() {
                amountOfPeople = 22,
                bookingTime = DateTime.Now,
                cus = cus,
                date = DateTime.Now.AddDays(7.0).Date,
                emp = em,
                er = er
                

            };


            //Act
            bc.CreateBooking(book.emp.employeeID, book.cus.username, book.er.escapeRoomID, book.bookingTime, book.amountOfPeople, book.date);
            hostBook = bc.get(cus, er, book.date);

            //Assert
            Assert.AreEqual(book.date, hostBook.date);
            Assert.AreEqual(book.emp.employeeID, hostBook.emp.employeeID);
            Assert.AreEqual(book.cus.username, hostBook.cus.username);

            bc.Delete(cus, er, book.date, book.emp, book.amountOfPeople, book.bookingTime);
        }

        [TestMethod]
        public void ViewResult() {

            //Arrange
            //BookingController bc = new BookingController();


            //Act
            //ViewResult result = bc. as ViewResult;

            //Assert
            //Assert.IsNotNull(result);

        }
    }
}
