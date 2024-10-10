using AdminHRM.Server.Dtos;

namespace AdminHRM.Dtos.Leaves
{
    public class LeaveDto : LeaveCreateDto
    {
        public Guid Id { get; set; }
        public SubUnitDto SubUnit { get; set; }
    }
}
