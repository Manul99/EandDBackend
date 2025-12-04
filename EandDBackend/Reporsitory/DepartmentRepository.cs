using EandDBackend.DTOs;
using EandDBackend.Interfaces.Repositories;
using EandDBackend.Models;
using System.Data.SqlClient;

namespace EandDBackend.Reporsitory
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly string _connectionString;
        public DepartmentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

      
        // Create or Update department 
        public async Task<int> AddOrUpdateDepartment(DepartmentDto department)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spDepartmentsAddEdit", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Input parameters
                    cmd.Parameters.AddWithValue("@numDepatmentId", department.numDepatmentId == 0 ? (object)DBNull.Value : department.numDepatmentId);
                    cmd.Parameters.AddWithValue("@varDepartmentCode", department.varDepartmentCode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@varDepartmentName", department.varDepartmentName ?? (object)DBNull.Value);

                    // Output parameter
                    SqlParameter outParam = new SqlParameter("@outParam", System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outParam);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    // Return the output parameter (-1 = duplicate, 0 = error, 1 = success)
                    return (int)outParam.Value;
                }
            }
        }


        // Get all active departments
        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            var departments = new List<Department>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM Departments WHERE bitActive = 1";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    await conn.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Department dept = new Department
                            {
                                numDepatmentId = reader["numDepatmentId"] != DBNull.Value ? Convert.ToInt32(reader["numDepatmentId"]) : 0,
                                varDepartmentCode = reader["varDepartmentCode"] != DBNull.Value ? reader["varDepartmentCode"].ToString() : null,
                                varDepartmentName = reader["varDepartmentName"] != DBNull.Value ? reader["varDepartmentName"].ToString() : null,
                                bitActive = reader["bitActive"] != DBNull.Value ? (bool?)Convert.ToBoolean(reader["bitActive"]) : null,
                                dteCreatedAt = reader["dteCreatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["dteCreatedAt"]) : null,
                                numCreatedBy = reader["numCreatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["numCreatedBy"]) : null,
                                dteUpdatedAt = reader["dteUpdatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["dteUpdatedAt"]) : null,
                                numUpdatedBy = reader["numUpdatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["numUpdatedBy"]) : null
                            };
                            departments.Add(dept);
                        }
                    }
                }
            }

            return departments;
        }

        // Get department by ID
        public async Task<Department?> GetDepartmentById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM Departments WHERE numDepatmentId = @id AND bitActive = 1";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    await conn.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Department dept = new Department
                            {
                                numDepatmentId = reader["numDepatmentId"] != DBNull.Value ? Convert.ToInt32(reader["numDepatmentId"]) : 0,
                                varDepartmentCode = reader["varDepartmentCode"] != DBNull.Value ? reader["varDepartmentCode"].ToString() : null,
                                varDepartmentName = reader["varDepartmentName"] != DBNull.Value ? reader["varDepartmentName"].ToString() : null,
                                bitActive = reader["bitActive"] != DBNull.Value ? (bool?)Convert.ToBoolean(reader["bitActive"]) : null,
                                dteCreatedAt = reader["dteCreatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["dteCreatedAt"]) : null,
                                numCreatedBy = reader["numCreatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["numCreatedBy"]) : null,
                                dteUpdatedAt = reader["dteUpdatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["dteUpdatedAt"]) : null,
                                numUpdatedBy = reader["numUpdatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["numUpdatedBy"]) : null
                            };
                            return dept;
                        }
                    }
                }
            }
            return null;

        }

        //Delete department
        public async Task<bool> DeleteDepartment(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Departments SET bitActive = 0, dteUpdatedAt = GETDATE(),numUpdatedBy = 1 WHERE numDepatmentId = @id";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    await conn.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }



    }
}
