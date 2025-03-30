namespace AdminHRM.Dtos
{
    public class EmployeeDto : EmployeeCreateDto
    {
        public Guid Id { get; set; }
        public string? SubUnitName { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public EmployeeParentChildDto? SupperVisor { get; set; }
        public IEnumerable<EmployeeParentChildDto>? EmployeeChildrens { get; set; }
    }
    public class EmployeeParentChildDto
    {
        public Guid? Id { get; set; }
        public string FullName { get; set; }
    }
}
