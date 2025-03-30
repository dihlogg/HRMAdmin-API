namespace AdminHRM.Dtos.Leaves
{
    public class LeaveStatusDto : LeaveStatusCreateDto
    {
        public Guid Id { get; set; }
    }
    public class LeaveStatusCreateDto
    {
        public string? StatusName { get; set; }
        public int DisplayOrder { get; set; }
    }
}
