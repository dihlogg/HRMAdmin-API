using System.ComponentModel.DataAnnotations;
using AdminHRM.Server.Entities;

namespace AdminHRM.Entities
{
    public class RequestStatus : BaseEntities
    {
        public string? StatusName { get; set; }
        public int DisplayOrder { get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; }
    }
}
