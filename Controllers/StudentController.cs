using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnetcore_api.Models;
using System.Diagnostics;

namespace dotnet_api.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        // GET api/student
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            List<Student> result = null;
            using (var db = new DatabaseContext()) 
            {
                result = db.Students.ToList();
            }
            return result;
        }

        // GET api/student/5
        [HttpGet("{id}")]
        public Student Get(int id)
        {
            Student result = null;
            using (var db = new DatabaseContext()) 
            {
                result = db.Students.Where(a => a.Id == id).FirstOrDefault();
            }
            return result;
        }

        // POST api/student
        [HttpPost]
        public ServiceResult Post([FromBody]Student value)
        {
            ServiceResult result = new ServiceResult();
            if (value == null) 
            {
                result.IsSuccess = false;
                result.Message = "Post value is null";
                return result;
            }
            using (var db = new DatabaseContext()) 
            {
                try 
                {
                    db.Students.Add(value);
                    db.SaveChanges();
                    result.IsSuccess = true;
                    result.Message = string.Empty;
                } 
                catch (Exception ex) 
                {
                    Debug.WriteLine(ex.Message);
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        // PUT api/student/5
        [HttpPut("{id}")]
        public ServiceResult Put(int id, [FromBody]Student value)
        {
            ServiceResult result = new ServiceResult();
            if (value == null) 
            {
                result.IsSuccess = false;
                result.Message = "Post value is null";
                return result;
            }
            using (var db = new DatabaseContext()) 
            {
                try 
                {
                    var m = (from a in db.Students where a.Id == id select a).FirstOrDefault();
                    if (m != null) 
                    {
                        m.Name = value.Name;
                        m.Grade = value.Grade;
                        m.ClassName = value.ClassName;
                        db.SaveChanges();
                        result.IsSuccess = true;
                        result.Message = string.Empty;
                    }
                    else 
                    {
                        result.IsSuccess = false;
                        result.Message = "Cannot find id(" + id + ") in database";
                    }
                } 
                catch (Exception ex) 
                {
                    Debug.WriteLine(ex.Message);
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        // DELETE api/student/5
        [HttpDelete("{id}")]
        public ServiceResult Delete(int id)
        {
            ServiceResult result = new ServiceResult();
            using (var db = new DatabaseContext()) 
            {
                var d = (from a in db.Students where a.Id == id select a).FirstOrDefault();
                if (d != null) 
                {
                    try 
                    {
                        db.Students.Remove(d);
                        db.SaveChanges();
                        result.IsSuccess = true;
                        result.Message = string.Empty;
                    } 
                    catch (Exception ex) 
                    {
                        Debug.WriteLine(ex.Message);
                        result.IsSuccess = false;
                        result.Message = ex.Message;
                    }
                }
                else 
                {
                    result.IsSuccess = false;
                    result.Message = "Cannot find id(" + id + ") in database";
                }
            }
            return result;
        }
    }
}
