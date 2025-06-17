using Egventoryv7BackEnd.Contracts.ServicesContracts;
using Egventoryv7BackEnd.Models.EmployeeModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Egventoryv7BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Administrator")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService service)
        {
            _employeeService = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployeesAsync()
        {
            var employees = await _employeeService.GetAllEmployees();
            if (!employees.Any() || employees.Count() == 0)
                return NotFound("No employees found.");
            return Ok(employees);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid entry.");
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee is null)
                return NotFound("Employee does not exist.");
            return employee;
        }
        [HttpGet("LastName")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeesByLastNameAsync(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
                return BadRequest("Invalid entry");
            var employees = await _employeeService.GetEmployeesByLastNameAsync(lastName);
            if (!employees.Any() || employees.Count() == 0)
                return NotFound($"No employees with {lastName}");
            return Ok(employees);
        }
        [HttpGet("RolePosition")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeesByRolePositionAsync(string rolePosition)
        {
            var employees = await _employeeService.GetEmployeesByRolePositionAsync(rolePosition);
            if (!employees.Any() || employees.Count() == 0)
                return NotFound($"No {rolePosition} currently registered.");
            return Ok(employees);
        }
        [HttpGet("Display")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetDisplayEmployeesAsync()
        {
            var employees = await _employeeService.GetDisplayEmployees();
            if (!employees.Any() || employees.Count() == 0)
                return NotFound("No employees currently registered.");
            return Ok(employees);
        }
        [HttpPost]
        public async Task<ActionResult<CreateEmployeeResponse>> CreateEmployeeAsync(CreateEmployeeRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new CreateEmployeeResponse { EmployeeId = 0, Message = "Invalid details." });
            var createEmployee = await _employeeService.CreateEmployeeAsync(request);
            if (createEmployee == 0)
                return BadRequest(new CreateEmployeeResponse { EmployeeId = createEmployee, Message = "Employee cannot be created." });
            return Ok(new CreateEmployeeResponse { EmployeeId = createEmployee, Message = "Employee added." });
        }
        [HttpDelete]
        public async Task<ActionResult<object>> DeleteEmployeeByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest(new { Message = "Invalid entry." });
            var deleteEmployee = await _employeeService.DeleteEmployeeByIdAsync(id);
            if (!deleteEmployee)
                return BadRequest(new { Message = "Employee cannot be deleted."});
            return Ok(new { Message = "Employee deleted."});
        }
        [HttpPut]
        public async Task<ActionResult<object>> UpdateEmployeeAsync(UpdateEmployeeRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Invalid entry details" });
            var updateEmployee = await _employeeService.UpdateEmployeeAsync(request);
            if (!updateEmployee)
                return BadRequest(new { Message = "Employee cannot be updated." });
            return Ok(new { Message = "Employee updated." });
        }
    }
}
