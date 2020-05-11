using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MAPMA_WebClient.ServiceLayer;
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
            BookingService bs = new BookingService();
            CustomerService cs = new CustomerService();
            EscapeRoomService es = new EscapeRoomService();
            EmployeeService ems = new EmployeeService();

            MAPMA_WebClient.CusRef.Customer cus = cs.GetCustomer("Anorak");
            MAPMA_WebClient.EmpRef.Employee em = ems.GetEmployee(1);
            MAPMA_WebClient.EscRef.EscapeRoom er = es.GetEscapeRoom(1);
            Customer customer = new Customer() {
                customerNo = cus.customerNo,
                firstName = cus.firstName,
                lastName = cus.lastName,
                mail = cus.mail,
                password = cus.password,
                phone = cus.phone,
                username = cus.username
            };
            Employee employee = new Employee() {
                address = em.address,
                cityName = em.cityName,
                employeeID = em.employeeID,
                firstName = em.firstName,
                lastName = em.lastName,
                mail = em.mail,
                phone = em.phone,
                region = em.region,
                zipcode = em.zipcode
            };
            EscapeRoom escapeRoom = new EscapeRoom() {
                cleanTime = er.cleanTime,
                description = er.description,
                escapeRoomID = er.escapeRoomID,
                maxClearTime = er.maxClearTime,
                name = er.name,
                price = er.price,
                rating = er.rating
            };
            Booking hostBook;
            Booking book = new Booking() {
                amountOfPeople = 22,
                bookingTime = DateTime.Now.TimeOfDay,
                cus = customer,
                date = DateTime.Now.AddDays(7.0).Date,
                emp = employee,
                er = escapeRoom
            };


            //Act
            bs.CreateBooking(book.emp.employeeID, book.cus.username, book.er.escapeRoomID, book.bookingTime, book.amountOfPeople, book.date);
            hostBook = bs.GetBooking(book.er.escapeRoomID, book.cus.username, book.date);

            //Assert
            Assert.AreEqual(book.date, hostBook.date);
            Assert.AreEqual(book.emp.employeeID, hostBook.emp.employeeID);
            Assert.AreEqual(book.cus.username, hostBook.cus.username);

            bs.DeleteBooking(book.emp.employeeID, book.cus.username, book.er.escapeRoomID, book.bookingTime, book.amountOfPeople, book.date );

            Assert.IsNull(bs.GetBooking(book.er.escapeRoomID, book.cus.username, book.date));
        }
    }
}
