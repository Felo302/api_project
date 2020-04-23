using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;

namespace WebApplication1.Controllers
{
    public class EmployeesController : ApiController
    {
        public IEnumerable<FoodLion> Get()
        {
            using(employeesEntities entities = new employeesEntities())
            {
                //this project was created to learn how to catch the data from an sql table 
                //example: http:/ /localhost:49891/api/employees
                return entities.FoodLions.ToList();
            }
        }
        public FoodLion Get(int id)
        {
            using (employeesEntities entities = new employeesEntities())
            {
                return entities.FoodLions.FirstOrDefault(e => e.RecordID == id);
                //example: http:/ /localhost:49891/api/employees/3
            }
        }
    }
}
