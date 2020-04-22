using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAPMA_WebClient.EmpRef;

namespace MAPMA_WebClient.ServiceLayer
{
    public class EmployeeService
    {
        public EmployeeService() {
            
        }
        //IemplyeeServices skal have ændret Navn til IEmployeeServices
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