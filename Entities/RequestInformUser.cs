using System.ComponentModel.DataAnnotations;
using AdminHRM.Server.Entities;

namespace AdminHRM.Entities
{
    public class RequestInformUser
    {
        [Key]
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string LeaveId { get; set; }
        public Employee Employee { get; set; }
        public LeaveRequest LeaveRequest { get; set; }
    }
}
