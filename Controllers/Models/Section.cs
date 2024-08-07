namespace StudentManagementAPI.Controllers.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string SectionName { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int ClassId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Class? Class { get; set; }
    }
}
