namespace StudentManagementAPI.Controllers.Models
{
    public enum Status
    {
        Inactive = 0,
        Active = 1,
        Archived = 2
    }

    public class ClassModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public Status Status { get; set; } = Status.Active;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        private DateTime _updatedAt = DateTime.Now;
        public DateTime UpdateAt
        {
            get => _updatedAt;
            private set => _updatedAt = value;
        }

        public DateTime? DeletedAt { get; set; }

        public void Update(string? name, string? description, Status status)
        {
            Name = name;
            Description = description;
            Status = status;
            UpdateAt = DateTime.Now;
        }

    }
}
