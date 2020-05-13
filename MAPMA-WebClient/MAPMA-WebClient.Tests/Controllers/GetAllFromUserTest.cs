using System;
using System.Collections.Generic;
using MAPMA_WebClient.ServiceLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAPMA_WebClient.Tests.Controllers
{
    [TestClass]
    public class GetAllFromUserTest
    {
        [TestMethod]
        public void TestMethod1 ( )
        {
            //Arrange
            BookingService bs = new BookingService();
            List<BookRef.Booking> books = new List<BookRef.Booking>();
            IEnumerable<BookRef.Booking> tempBooks;

            //Act
            tempBooks = bs.GetAllBookingFromUser("SirLol");
            foreach (BookRef.Booking book in tempBooks)
            {
                books.Add(book);
            }

            //Assert
            Assert.IsTrue(books.Count != 0);

        }
    }
}
