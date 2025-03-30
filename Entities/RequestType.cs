using System.ComponentModel.DataAnnotations;
using AdminHRM.Server.Entities;

namespace AdminHRM.Entities
{
    public class RequestType : BaseEntities
    {
        public string? TypeName { get; set; }
        public int DisplayOrder { get; set; }
        public string? CardId { get; set; }
        public DashboardCard? Card { get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; }
    }
}
