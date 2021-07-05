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


        [HttpGet]
        [Route("departments")]
        public ActionResult GetDepartments()
        {
            return Ok();
        }

    }
}
