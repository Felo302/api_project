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
                ///example: http:/ /localhost:49891/api/employees
                return entities.FoodLions.ToList();
            }
        }
        public HttpResponseMessage Get(int id)
        {
            using (employeesEntities entities = new employeesEntities())
            { 
                ///example: http:/ /localhost:49891/api/employees/3
                var entity = entities.FoodLions.FirstOrDefault(e => e.RecordID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "employee with ID: " + id);
                }
            }
        }
        public HttpResponseMessage Post([FromBody]FoodLion employee)
        {
            using (employeesEntities entities = new employeesEntities())
            {
                try
                {
                    entities.FoodLions.Add(employee);
                    entities.SaveChanges();
                    // this code changes the status form 202 to 201 created 
                    // and changes location to http:// localhost:49891/api/employees/10
                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.RecordID.ToString());
                    return message;
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
    }
}
