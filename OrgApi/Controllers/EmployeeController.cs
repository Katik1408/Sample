using Microsoft.AspNetCore.Mvc;
using OrgApi.Models;
using System.Linq;

namespace OrgApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly OrganizationContext _context;
        //const  // Compile Time Constant
        //    readonly // Runtime Constant

        const int a = 7;
        public EmployeeController(OrganizationContext _context)
        {
            this._context = _context;
        }

        [HttpGet]
        [Route("employees")]
        public ActionResult GetEmployees()
        {
            var result = _context.Employees.ToList();
            return Ok(result);
        }

        [HttpGet, Route("emp")]
        public ActionResult GetEmployee([FromQuery] int id)
        {
            var result = _context.Employees.Where(w => w.Empid == id).Select(s => s.EmployeeName).ToList();
            return Ok(result);
        }


        [HttpPost, Route("CreateNewEmployee")]
        public ActionResult CreateEmployee([FromBody] Employee emp)
        {

            _context.Employees.Add(new Employee()
            {
                Age = emp.Age,
                EmployeeName = emp.EmployeeName,
                //DeptId = _context.Departments.Where(w => w.Id == 1).Select(s => s.Id).ToString(),
            });
            _context.SaveChanges();
            return Ok("New Employee Created");
        }

        [HttpDelete]
        [Route("removeEmployee/{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null)
            {
                return BadRequest("No Employee with id " + id);
            }
            else
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
                return Ok("Delete Success");
            }
        }


        [HttpPut]
        [Route("updateEmployee/{id}")]
        public ActionResult UpdateEmployee(int id, [FromBody] Employee emp)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return BadRequest("No Employee with id " + id);
            }
            else
            {
                employee.Age = emp.Age;
                employee.Place = emp.Place;
                employee.EmployeeName = emp.EmployeeName;
                employee.DeptId = emp.DeptId;
                employee.DateOfJoining = emp.DateOfJoining;

                _context.Employees.Update(employee);
                _context.SaveChanges();
                return Ok("Updated Successfully");

            }
        }




        [HttpGet]
        [Route("departments")]
        public ActionResult GetDepartments()
        {
            return Ok();
        }

    }
}
