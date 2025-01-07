using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.DTOs;
using BLL.Services;
namespace webform.Controllers

{
    public class StudentController : ApiController
    {
        [HttpGet]
        [Route("api/students/all")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = StudentService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/students/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                var data = StudentService.Get(id);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Student not found");

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("api/students/create")]
        public HttpResponseMessage Create([FromBody] StudentDTO student)
        {
            try
            {
                if (student == null || string.IsNullOrWhiteSpace(student.StudentName))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid student data.");
                }

                StudentService.Create(student);
                return Request.CreateResponse(HttpStatusCode.Created, "Student successfully created!!");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpDelete]
        [Route("api/students/delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                StudentService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Student successfully deleted");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
