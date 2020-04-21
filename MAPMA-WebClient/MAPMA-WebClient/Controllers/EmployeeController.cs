using MAPMA_WebClient.EmpRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAPMA_WebClient.Controllers
{
    public class EmployeeController : Controller {

        public ActionResult GetEmployee ( ) {

            return View();
            }

        [HttpPost]
        public ActionResult GetEmployee(int id) {

            IEmplyeeServices es = new EmplyeeServicesClient();
            Employee emp = es.Get(id);
            return View(emp);
        }
    }
}