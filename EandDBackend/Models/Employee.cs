namespace EandDBackend.Models
{
    public class Employee
    {
        public int? numEmployeeId { get; set; }
        public string? varFirstName { get; set; }
        public string? varLastName { get; set; }
        public string? varEmail { get; set; }
        public DateTime? dteDateOfBirth { get; set; }
        public int? numAge { get; set; }
        public decimal? numSalary { get; set; }
        public int? numDepatmentId { get; set; }
        public bool? bitActive { get; set; }
        public DateTime? dteCreatedAt { get; set; }
        public int? numCreatedBy { get; set; }
        public DateTime? dteUpdatedAt { get; set; }
        public int? numUpdatedBy { get; set; }
    }
}

