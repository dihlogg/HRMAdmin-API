namespace AdminHRM.Dtos.Leaves;

public class LeaveCreateDto
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? LeaveStatus { get; set; }
    public string? LeaveType { get; set; }
    public Guid EmployeeId { get; set; }
}
