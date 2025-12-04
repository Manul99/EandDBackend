using EandDBackend.DTOs;
using EandDBackend.Interfaces.Repositories;
using EandDBackend.Models;
using System.Data.SqlClient;

namespace EandDBackend.Reporsitory
{
    
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;
        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Add or Update Employee
        public async Task<int> AddOrUpdateEmployees(EmployeeDto employee)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spEmployeesAddEdit", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Input parameters
                    cmd.Parameters.AddWithValue("@numEmployeeId", (object?)employee.numEmployeeId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@varFirstName", employee.varFirstName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@varLastName", employee.varLastName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@varEmail", employee.varEmail ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@dteDateOfBirth", employee.dteDateOfBirth ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@numAge", employee.numAge != 0 ? employee.numAge : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@numSalary", employee.numSalary != 0 ? employee.numSalary : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@numDepatmentId", employee.numDepatmentId != 0 ? employee.numDepatmentId : (object)DBNull.Value);

                    // Output parameter
                    SqlParameter outParam = new SqlParameter("@outParam", System.Data.SqlDbType.Int)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outParam);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    // Return the output parameter
                    // 1 = Inserted, 2 = Updated, -1 = Duplicate, 0 = Error
                    return (int)outParam.Value;
                }
            }
        }

        // Get all active employees
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var employees = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM Employees WHERE bitActive = 1";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    await conn.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Employee emp = new Employee
                            {
                                numEmployeeId = reader["numEmployeeId"] != DBNull.Value ? Convert.ToInt32(reader["numEmployeeId"]) : 0,
                                varFirstName = reader["varFirstName"] != DBNull.Value ? reader["varFirstName"].ToString() : null,
                                varLastName = reader["varLastName"] != DBNull.Value ? reader["varLastName"].ToString() : null,
                                varEmail = reader["varEmail"] != DBNull.Value ? reader["varEmail"].ToString() : null,
                                dteDateOfBirth = reader["dteDateOfBirth"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["dteDateOfBirth"]) : null,
                                numAge = reader["numAge"] != DBNull.Value ? (int?)Convert.ToInt32(reader["numAge"]) : null,
                                numSalary = reader["numSalary"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["numSalary"]) : null,
                                numDepatmentId = reader["numDepatmentId"] != DBNull.Value ? (int?)Convert.ToInt32(reader["numDepatmentId"]) : null,
                                bitActive = reader["bitActive"] != DBNull.Value ? (bool?)Convert.ToBoolean(reader["bitActive"]) : null,
                                dteCreatedAt = reader["dteCreatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["dteCreatedAt"]) : null,
                                numCreatedBy = reader["numCreatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["numCreatedBy"]) : null,
                                dteUpdatedAt = reader["dteUpdatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["dteUpdatedAt"]) : null,
                                numUpdatedBy = reader["numUpdatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["numUpdatedBy"]) : null
                            };
                            employees.Add(emp);
                        }
                    }
                }
            }

            return employees;
        }

        // Get employee by ID
        public async Task<Employee?> GetEmployeeById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM Employees WHERE numEmployeeId = @id AND bitActive = 1";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    await conn.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Employee emp = new Employee
                            {
                                numEmployeeId = reader["numEmployeeId"] != DBNull.Value ? Convert.ToInt32(reader["numEmployeeId"]) : 0,
                                varFirstName = reader["varFirstName"] != DBNull.Value ? reader["varFirstName"].ToString() : null,
                                varLastName = reader["varLastName"] != DBNull.Value ? reader["varLastName"].ToString() : null,
                                varEmail = reader["varEmail"] != DBNull.Value ? reader["varEmail"].ToString() : null,
                                dteDateOfBirth = reader["dteDateOfBirth"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["dteDateOfBirth"]) : null,
                                numAge = reader["numAge"] != DBNull.Value ? (int?)Convert.ToInt32(reader["numAge"]) : null,
                                numSalary = reader["numSalary"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["numSalary"]) : null,
                                numDepatmentId = reader["numDepatmentId"] != DBNull.Value ? (int?)Convert.ToInt32(reader["numDepatmentId"]) : null,
                                bitActive = reader["bitActive"] != DBNull.Value ? (bool?)Convert.ToBoolean(reader["bitActive"]) : null,
                                dteCreatedAt = reader["dteCreatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["dteCreatedAt"]) : null,
                                numCreatedBy = reader["numCreatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["numCreatedBy"]) : null,
                                dteUpdatedAt = reader["dteUpdatedAt"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["dteUpdatedAt"]) : null,
                                numUpdatedBy = reader["numUpdatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(reader["numUpdatedBy"]) : null
                            };
                            return emp;
                        }
                    }
                }
            }

            return null;
        }
        //Delete Employee
        public async Task<bool> DeleteEmployees(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Employees SET bitActive = 0, dteUpdatedAt = GETDATE(),numUpdatedBy = 1 WHERE numEmployeeId = @id";

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
