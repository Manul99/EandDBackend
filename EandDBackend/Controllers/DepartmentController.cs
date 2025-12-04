using EandDBackend.DTOs;
using EandDBackend.Interfaces.Services;
using EandDBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace EandDBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddOrUpdateDepartment([FromBody] DepartmentDto department)
        {
            if (department == null)
                return BadRequest("Department data is null.");

            try
            {
          
                int result = await _departmentService.AddOrUpdateDepartment(department);

                // Handle based on the output parameter
                return result switch
                {
                    1 => Ok(new { data = 1, message = "Department saved successfully!" }),
                    2 => Ok(new { data = 2, message = "Department updated successfully!" }),
                    -1 => BadRequest("Duplicate department exists!"),
                    0 => StatusCode(500, new { data = 0, message = "An error occurred while saving the department." }),
                    _ => StatusCode(500, new { data = -99, message = "Unknown error occurred." })
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var departments = await _departmentService.GetAllDepartments();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("getbyID/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var department = await _departmentService.GetDepartmentById(id);
                if (department == null)
                {
                    return NotFound($"Department with ID {id} not found.");
                }
                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                bool isDeleted = await _departmentService.DeleteDepartment(id);
                if (isDeleted)
                {
                    return Ok($"Department with ID {id} deleted successfully.");
                }
                else
                {
                    return NotFound($"Department with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }


    }
}
