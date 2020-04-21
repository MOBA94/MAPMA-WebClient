using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAPMA_WebClient.Tests.Controllers
{
    [TestClass]
    public class BookRoomTest
    {
        [TestMethod]
        public void CreateNewBookingTest ( )
        {
            //Arrange
            BookController bc = new BookController();
            CustomerController cc = new CustomerController();
            EscapeRoomController ec = new EscapeRoomController();
            Employee emc = new EmployeeController();
            Customer cus = cc.Get("Anorak");
            Employee em = emc.Get(2);
            EscapeRoom er = ec.GetForOwner(3);
            Booking hostBook;
            Booking book = new Booking()
            {
                AmountOfPeople = 22,
                BookingTime = DateTime.Now,
                Cus = cus,
                Date = DateTime.Now.AddDays(7.0).Date,
                Emp = em,
                Er = er

            };


            //Act
            bc.Create(book.Emp, book.Cus, book.Er, book.BookingTime, book.AmountOfPeople, book.Date);
            hostBook = bc.Get(cus, er, book.Date);

            //Assert
            Assert.AreEqual(book.Date, hostBook.Date);
            Assert.AreEqual(book.Emp.EmloyeeID, hostBook.Emp.EmployeeID);
            Assert.AreEqual(book.Cus.Username, hostBook.Cus.Username);

            bc.Delete(cus, er, book.Date, book.Emp, book.AmountOfPeople, book.BookingTime);
        }

        [TestMethod]
        public void ViewResult() {

            //Arrange
            BookController bc = new BookController();


            //Act
            ViewResult result = bc.Book() as ViewResult;

            //Assert
            Assert.IsNotNull(result);

        }
    }
}
