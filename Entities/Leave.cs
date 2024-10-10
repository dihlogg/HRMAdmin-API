using AdminHRM.Server.Entities;

namespace AdminHRM.Entities
{
    public class LeaveList
    {
        public int Total { get; set; }
        public List<Leave> LeaveListDetails { get; set; }
    }

    public class Leave : BaseEntities
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? LeaveStatus { get; set; }
        public string? LeaveType { get; set; }
        public string? EmployeeName { get; set; }
        public Guid? SubUnitId { get; set; }
        public SubUnit? SubUnits { get; set; }
        public Guid? EmployeeId { get; set; }
        public IList<Employee> Employees { get; set; }
    }
}
