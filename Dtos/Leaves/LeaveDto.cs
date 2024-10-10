using AdminHRM.Server.Dtos;

namespace AdminHRM.Dtos.Leaves
{
    public class LeaveDto : LeaveCreateDto
    {
        public Guid Id { get; set; }
        public string? EmployeeName { get; set; }
        public string? SubName { get; set; }
        //public SubUnitDto SubUnit { get; set; }
    }
    public class LeaveListDto
    {
        public int Total { get; set; }
        public List<LeaveDto> LeaveListDetailDto { get; set; }
    }
}
