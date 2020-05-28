using MAPMA_WebClient.EmpRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    public class EmployeeController : Controller {

        /// <summary>
        /// An empty version of GetEmployee
        /// </summary>
        /// <returns>Returns the view GetEmployee</returns>
        public ActionResult GetEmployee ( ) {

            return View();
            }

        /// <summary>
        /// Gets an employee to pass to view
        /// </summary>
        /// <param name="id">An employees ID</param>
        /// <returns>Returns the view GetEmployee of the employee with the ID</returns>
        [HttpPost]
        public ActionResult GetEmployee(int id) {

            IEmplyeeServices es = new EmplyeeServicesClient();
            Employee emp = es.Get(id);
            return View(emp);
        }
    }
}