using static System.Collections.Specialized.BitVector32;

namespace StudentManagementAPI.Controllers.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string? ClassName { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<Section>? Sections { get; set; }
    }
}
