using System.ComponentModel.DataAnnotations;

namespace AdminHRM.Dtos.Leaves
{
    public class LeaveDashboardCardDto : LeaveDashboardCreateDto
    {
    }
    public class LeaveDashboardCreateDto
    {
        [Required]
        public string? CardId { get; set; }
        public string CardName { get; set; }
        public string? CardIcon { get; set; }
        public int DisplayOrder { get; set; }
    }
}
