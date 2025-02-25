namespace AdminHRM.Dtos.Leaves
{
    public class LeaveReasonDto : LeaveReasonCreateDto
    {
        public Guid Id { get; set; }
    }
    public class LeaveReasonCreateDto
    {
        public string? ReasonName { get; set; }
        public int DisplayOrder { get; set; }
    }
}
