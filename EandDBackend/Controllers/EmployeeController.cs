using EandDBackend.DTOs;
using EandDBackend.Interfaces.Services;
using EandDBackend.Service;
using Microsoft.AspNetCore.Mvc;

namespace EandDBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddOrUpdateEmployees([FromBody] EmployeeDto employee)
        {
            if (employee == null)
                return BadRequest("Department data is null.");

            try
            {
               
                int result = await _employeeService.AddOrUpdateEmployees(employee);

                // Handle based on the output parameter
                return result switch
                {
                    1 => Ok(new { data = 1, message = "Employee saved successfully!" }),//save successful
                    2 => Ok(new { data = 2, message = "Employee updated successfully!" }),//Update successful
                    -1 => BadRequest("Duplicate Employee exists."), // Duplicate Employee
                    0 => StatusCode(500, "An error occurred while saving the Employee."), // SP error
                    _ => StatusCode(500, "Unknown error occurred.")  // Any other unexpected value
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getbyID/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeById(id);
                if (employee == null)
                    return NotFound($"Employee with ID {id} not found.");
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEmployees(int id)
        {
            try
            {
                bool isDeleted = await _employeeService.DeleteEmployees(id);
                if (isDeleted)
                {
                    return Ok($"Employee with ID {id} deleted successfully.");
                }
                else
                {
                    return NotFound($"Employee with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }
    

}
