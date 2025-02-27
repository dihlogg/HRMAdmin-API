namespace AdminHRM.Dtos.Leaves
{
    public class RequestTypeDto : RequestTypeCreateDto
    {
        public Guid Id { get; set; }
    }
    public class RequestTypeCreateDto
    {
        public string? TypeName { get; set; }
        public int DisplayOrder { get; set; }
    }
}
