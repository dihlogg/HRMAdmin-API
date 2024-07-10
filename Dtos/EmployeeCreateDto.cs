using AdminHRM.Server.Entities;

namespace AdminHRM.Server.Dtos
{
    public class EmployeeCreateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string JobTitle { get; set; }
        public string Status { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid SubUnitId { get; set; }
        public string SubUnitName { get; set; }
    }
}
