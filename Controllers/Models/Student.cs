namespace StudentManagementAPI.Controllers.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime Dob { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }

        public Class Class { get; set; }
        public Section Section { get; set; }
    }
}
