using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdminHRM.Server.Entities;

namespace AdminHRM.Entities
{
    public class LeaveRequest : BaseEntities
    {
        public Guid RequestTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? DetailReason { get; set; }
        public DateTime? ExpectApprove { get; set; }
        public Guid? ApprovedId { get; set; }
        public Guid? SuppervisorId { get; set; }
        public string? ReasonId { get; set; }
        public string? StatusId { get; set; }
        public enum PartialDay
        {
            FullDay,
            Morning,
            Afternoon
        }
        public RequestType RequestType { get; set; }
        public RequestReason RequestReason { get; set; }
        public RequestStatus RequestStatus { get; set; }

        [ForeignKey("ApprovedId")]
        public Employee? ApprovedUser { get; set; }

        [ForeignKey("SupervisorId")]
        public Employee? Suppervisor { get; set; }

        [ForeignKey("CardId")]
        public DashboardCard? Card { get; set; }
        public string? CardId   { get; set; }
        public ICollection<RequestInformUser> InformUsers { get; set; }
    }
}
