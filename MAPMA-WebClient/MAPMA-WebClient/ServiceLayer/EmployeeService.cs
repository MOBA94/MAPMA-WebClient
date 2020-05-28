using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAPMA_WebClient.EmpRef;

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
    public class EmployeeService
    {

        /// <summary>
        /// Constructor for EmployeeService
        /// </summary>
        public EmployeeService() {
            
        }

        /// <summary>
        /// Gets an employee
        /// </summary>
        /// <param name="id"> An employees ID</param>
        /// <returns>returns an employee with the id</returns>
        public Employee GetEmployee(int id) {
            IEmplyeeServices Empserv = new EmplyeeServicesClient();
            try {
                return Empserv.Get(id);
            }
            catch (NullReferenceException NE) {
                Console.WriteLine(NE);
                Console.ReadLine();
                return null;
            }
        }
    }

}