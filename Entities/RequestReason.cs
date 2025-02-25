using System.ComponentModel.DataAnnotations;
using AdminHRM.Server.Entities;

namespace AdminHRM.Entities
{
    public class RequestReason : BaseEntities
    {
        public string? ReasonName { get; set; }
        public int DisplayOrder { get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; }
    }
}
