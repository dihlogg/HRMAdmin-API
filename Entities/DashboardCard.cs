using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdminHRM.Server.Entities;

namespace AdminHRM.Entities
{
    public class DashboardCard : BaseEntities
    {
        public string? CardId { get; set; }
        public string CardName { get; set; }
        public string? CardIcon {  get; set; }
        public int DisplayOrder { get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; }
        public ICollection<RequestType> RequestTypes { get; set; }

    }
}
