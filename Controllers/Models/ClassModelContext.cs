using Microsoft.EntityFrameworkCore;

namespace StudentManagementAPI.Controllers.Models
{
    public class ClassModelContext :  DbContext
    {
        public ClassModelContext(DbContextOptions<ClassModelContext> options) : base(options)
        {

        }
        public DbSet<ClassModel> ClassModels { get; set; }

    }
}
