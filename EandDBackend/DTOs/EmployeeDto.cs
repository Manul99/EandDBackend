namespace EandDBackend.DTOs
{
    public class EmployeeDto
    {
        public int? numEmployeeId { get; set; }
        public string? varFirstName { get; set; }
        public string? varLastName { get; set; }
        public string? varEmail { get; set; }
        public DateTime? dteDateOfBirth { get; set; }
        public int? numAge { get; set; }
        public decimal? numSalary { get; set; }
        public int? numDepatmentId { get; set; }
    }
}
