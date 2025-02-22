using System.ComponentModel.DataAnnotations;

namespace AdminHRM.Entities
{
    public class RequestType
    {
        [Key]
        public string? RequestId { get; set; }
        public string? TypeName { get; set; }
        public int DisplayOrder { get; set; }
        public string? CardId { get; set; }
        public DashboardCard? Card { get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; }
        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public Guid? CreateBy { get; set; }

        public Guid? UpdateBy { get; set; }
    }
}
