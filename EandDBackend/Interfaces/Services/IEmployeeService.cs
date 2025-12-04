using EandDBackend.DTOs;
using EandDBackend.Models;

namespace EandDBackend.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee?> GetEmployeeById(int id);
        Task<int> AddOrUpdateEmployees(EmployeeDto employee);
        Task<bool> DeleteEmployees(int id);
    }
}
