using EandDBackend.DTOs;
using EandDBackend.Interfaces.Repositories;
using EandDBackend.Interfaces.Services;
using EandDBackend.Models;

namespace EandDBackend.Service
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<int> AddOrUpdateEmployees(EmployeeDto employee)
        {
            return await _employeeRepository.AddOrUpdateEmployees(employee);
        }

        // Get all employees
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployees();

            var response = employees.Select(e => new Employee
            {
                numEmployeeId = e.numEmployeeId,
                varFirstName = e.varFirstName,
                varLastName = e.varLastName,
                varEmail = e.varEmail,
                dteDateOfBirth = e.dteDateOfBirth,
                numAge = e.numAge,
                numSalary = e.numSalary,
                numDepatmentId = e.numDepatmentId,
                bitActive = e.bitActive,
                dteCreatedAt = e.dteCreatedAt,
                numCreatedBy = e.numCreatedBy,
                dteUpdatedAt = e.dteUpdatedAt,
                numUpdatedBy = e.numUpdatedBy
            });

            return response;
        }

        // Get employee by ID
        public async Task<Employee?> GetEmployeeById(int id)
        {
            var e = await _employeeRepository.GetEmployeeById(id);

            if (e == null) return null;

            return new Employee
            {
                numEmployeeId = e.numEmployeeId,
                varFirstName = e.varFirstName,
                varLastName = e.varLastName,
                varEmail = e.varEmail,
                dteDateOfBirth = e.dteDateOfBirth,
                numAge = e.numAge,
                numSalary = e.numSalary,
                numDepatmentId = e.numDepatmentId,
                bitActive = e.bitActive,
                dteCreatedAt = e.dteCreatedAt,
                numCreatedBy = e.numCreatedBy,
                dteUpdatedAt = e.dteUpdatedAt,
                numUpdatedBy = e.numUpdatedBy
            };
        }

        public async Task<bool> DeleteEmployees(int id)
        {
            return await _employeeRepository.DeleteEmployees(id);
        }
    }
}
