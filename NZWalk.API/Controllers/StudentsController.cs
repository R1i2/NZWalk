using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace NZWalk.API.Controllers
{
    //https//localhost:portnumber/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        string[] studentName = new string[] { "Rishav", "Alok", "Rajat", "Kajal" };
        //Get : https//localhost:portnumber/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok(studentName);
        }
        //Post: https://localhost:portnumber/api/Students?name=something
        [HttpPost]
        public IActionResult PostStdentName(string name)
        {
            List<string> newstudentName = new List<string>();
            newstudentName.AddRange(studentName);
            newstudentName.Add(name);
            return Ok(newstudentName);
        }
    }
}
