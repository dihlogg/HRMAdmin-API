namespace AdminHRM.Dtos.Leaves
{
    public class LeaveRequestDto : LeaveRequestCreateDto
    {
        public Guid Id { get; set; }
    }
    public class LeaveRequestCreateDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? DetailReason { get; set; }
        public DateTime? ExpectApprove { get; set; }
        public enum PartialDay
        {
            FullDay,
            Morning,
            Afternoon
        }
    }
}
