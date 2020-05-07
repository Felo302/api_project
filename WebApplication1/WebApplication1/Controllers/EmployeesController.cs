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
        
        public HttpResponseMessage Get(string gender = "All") 
        {
            using(employeesEntities entities = new employeesEntities())
            {
                //this project was created to learn how to catch the data from an sql table 
                ///example: http:/ /localhost:49891/api/employees

                switch (gender.ToLower())
                {
                    // example:http ://localhost:49891/api/employees?gender=male
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.FoodLions.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.FoodLions.Where(x => x.Gender == "Male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.FoodLions.Where(x => x.Gender == "Female").ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Value for gender must be All, male or female" + gender +"is invalid.");
                }
               
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
        public HttpResponseMessage delete(int id)
        {
            using (employeesEntities entities = new employeesEntities())
            {
                //removes from FoodLion's collection
                try
                {
                    var entity = entities.FoodLions.Remove(entities.FoodLions.FirstOrDefault(e => e.RecordID == id));
                    entities.SaveChanges();
                    if (entity != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "employee with ID: " + id.ToString());
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
        //id checks the request from URI value
        //employee check the request from the body parameter
        public HttpResponseMessage put(int id, [FromBody]FoodLion employee)
        {
            using (employeesEntities entities = new employeesEntities())
            {
                try
                {

                // get the value of the table foodLion class
                var entity = entities.FoodLions.FirstOrDefault(e => e.RecordID == id);
                if (entity != null)
                {
                    entity.FirstName = employee.FirstName;
                    entity.FirstName = employee.LastName;
                    entity.Gender = employee.Gender;
                    entity.Salary = employee.Salary;

                    entities.SaveChanges();
                    return  Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "employee with ID: " + id.ToString());
                }
                }
                catch (Exception ex)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
    }
}
