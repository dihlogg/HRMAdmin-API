using AdminHRM.Server.Entities;

namespace AdminHRM.Entities
{
    public class Leave : BaseEntities
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? LeaveStatus { get; set; }
        public string? LeaveType { get; set; }
        // Foreign key for employee
        public Guid? EmployeeId { get; set; }

        // Navigation property for employee
        public Employee? Employees { get; set; }
    }
}
