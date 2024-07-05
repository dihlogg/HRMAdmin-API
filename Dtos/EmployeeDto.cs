namespace AdminHRM.Server.Dtos
{
    public class EmployeeDto : EmployeeCreateDto
    {
        public Guid Id { get; set; }
        public EmployeeParentChildDto? SupperVisor { get; set; }
        public IEnumerable<EmployeeParentChildDto> EmployeeChildrens { get; set; }
    }
    public class EmployeeParentChildDto
    {
        public Guid? Id { get; set; }
        public string FullName { get; set; }
    }
}
