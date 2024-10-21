namespace AdminHRM.Dtos.Leaves;

public class SearchLeaveDto
{
    public DateTime? FromDate {  get; set; }
    public DateTime? ToDate { get; set; }
    public string? LeaveType { get; set; }
    public string? LeaveStatus { get; set; }
    public string? EmployeeName { get; set; }
    public string? SubName { get; set; }
}
