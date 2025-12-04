namespace EandDBackend.Models
{
    public class Department
    {
        public int numDepatmentId { get; set; }
        public string? varDepartmentCode { get; set; }
        public string? varDepartmentName { get; set; }
        public bool? bitActive { get; set; }
        public DateTime? dteCreatedAt { get; set; }
        public int? numCreatedBy { get; set; }
        public DateTime? dteUpdatedAt { get; set; }
        public int? numUpdatedBy { get; set; }
    }
}
