using Microsoft.EntityFrameworkCore;
using MvcProject.Web.Models;

namespace MvcProject.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Define your DbSet for the User model
        public DbSet<User> Users { get; set; }

        public DbSet<TaskModel> Tasks { get; set; } // Represents the Task table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.TaskModel>()
           .Property(t => t.Tags)
           .HasMaxLength(500); // Example: limit serialized tags to 500 characters

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Admin", Email = "admin@example.com", Role = "Administrator" },
                new User { Id = 2, Name = "User1", Email = "user1@example.com", Role = "User" }
            );
            modelBuilder.Entity<Models.TaskModel>().HasData(
            new TaskModel { Id = 1, Title = "Prepare Marketing Presentation", Priority = "High", DueDate = new DateTime(2024, 1, 15, 18, 0, 0), CommentsCount = 12, Tags = new List<string> { "Marketing", "Presentation" } },
            new TaskModel { Id = 2, Title = "Submit Financial Report", Priority = "Urgent", DueDate = new DateTime(2024, 1, 10, 12, 0, 0), CommentsCount = 8, Tags = new List<string> { "Finance", "Report" } },
            new TaskModel { Id = 3, Title = "Team Meeting Preparation", Priority = "Medium", DueDate = new DateTime(2024, 1, 18, 14, 0, 0), CommentsCount = 4, Tags = new List<string> { "Meeting", "Team" } },
            new TaskModel { Id = 4, Title = "Update Software Documentation", Priority = "Low", DueDate = new DateTime(2024, 1, 22, 17, 0, 0), CommentsCount = 6, Tags = new List<string> { "Software", "Documentation" } },
            new TaskModel { Id = 5, Title = "Client Follow-up Calls", Priority = "High", DueDate = new DateTime(2024, 1, 12, 10, 0, 0), CommentsCount = 3, Tags = new List<string> { "Client", "Follow-up" } },
            new TaskModel { Id = 6, Title = "Organize Workspace", Priority = "Low", DueDate = new DateTime(2024, 1, 25, 15, 0, 0), CommentsCount = 0, Tags = new List<string> { "Organization", "Workspace" } },
            new TaskModel { Id = 7, Title = "Product Launch Planning", Priority = "Urgent", DueDate = new DateTime(2024, 1, 5, 11, 0, 0), CommentsCount = 15, Tags = new List<string> { "Product", "Launch" } }
        );
        }
    }
}
