using EandDBackend.DTOs;
using EandDBackend.Models;

namespace EandDBackend.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee?> GetEmployeeById(int id);
        Task<int> AddOrUpdateEmployees(EmployeeDto employee);

        Task<bool> DeleteEmployees(int id);
    }
}
