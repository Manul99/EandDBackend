using EandDBackend.DTOs;
using EandDBackend.Interfaces.Repositories;
using EandDBackend.Interfaces.Services;
using EandDBackend.Models;

namespace EandDBackend.Service
{
    public class DepartmentService : IDepartmentService
    {
        private IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<int> AddOrUpdateDepartment(DepartmentDto department)
        {
            return await _departmentRepository.AddOrUpdateDepartment(department);
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            var department =  await _departmentRepository.GetAllDepartments();
            var response = department.Select(p => new Department
            {
                numDepatmentId = p.numDepatmentId,
                varDepartmentCode = p.varDepartmentCode,
                varDepartmentName = p.varDepartmentName,
                bitActive = p.bitActive,
                dteCreatedAt = p.dteCreatedAt,
                numCreatedBy = p.numCreatedBy,
                numUpdatedBy = p.numUpdatedBy,
                dteUpdatedAt = p.dteUpdatedAt
            });
            return response;
        }
        public async Task<Department?> GetDepartmentById(int id)
        {
            var department = await _departmentRepository.GetDepartmentById(id);
            if(department == null) return null;
            var response = new Department
            {
                numDepatmentId = department.numDepatmentId,
                varDepartmentCode = department.varDepartmentCode,
                varDepartmentName = department.varDepartmentName,
                bitActive = department.bitActive,
                dteCreatedAt = department.dteCreatedAt,
                numCreatedBy = department.numCreatedBy,
                numUpdatedBy = department.numUpdatedBy,
                dteUpdatedAt = department.dteUpdatedAt
            };
            return response;
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            return await _departmentRepository.DeleteDepartment(id);
        }

    }
}
