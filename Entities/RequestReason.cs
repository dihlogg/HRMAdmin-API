using System.ComponentModel.DataAnnotations;

namespace AdminHRM.Entities
{
    public class RequestReason
    {
        [Key]
        public string ReasonId { get; set; }
        public string? ReasonName { get; set; }
        public int DisplayOrder { get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; }
        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public Guid? CreateBy { get; set; }

        public Guid? UpdateBy { get; set; }
    }
}
