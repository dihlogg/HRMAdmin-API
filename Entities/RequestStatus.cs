using System.ComponentModel.DataAnnotations;

namespace AdminHRM.Entities
{
    public class RequestStatus
    {
        [Key]
        public string StatusId { get; set; }
        public string? StatusName { get; set; }
        public int DisplayOrder { get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; }
        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public Guid? CreateBy { get; set; }

        public Guid? UpdateBy { get; set; }
    }
}
