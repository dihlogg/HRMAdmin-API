using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdminHRM.Server.Entities;

namespace AdminHRM.Entities
{
    public class RequestInformUser : BaseEntities
    {
        [ForeignKey(nameof(Employee))]
        public Guid? EmployeeId { get; set; }
        public Employee? Employees { get; set; }

        [ForeignKey(nameof(LeaveRequest))]
        public Guid? RequestId { get; set; }
        public LeaveRequest? LeaveRequest { get; set; }
    }
}
