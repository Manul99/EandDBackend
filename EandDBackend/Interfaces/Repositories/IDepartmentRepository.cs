using EandDBackend.DTOs;
using EandDBackend.Models;

namespace EandDBackend.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartments();
        Task<Department?> GetDepartmentById(int id);
        Task<int> AddOrUpdateDepartment(DepartmentDto department);

        Task<bool> DeleteDepartment(int id);

    }
}
