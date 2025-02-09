using System.ComponentModel.DataAnnotations;

namespace MvcProject.Web.Models
{
    public class TaskModel
    {
        [Key]
        public int Id { get; set; } // Primary key

        [Required]
        public string Title { get; set; } // Task title

        public string Priority { get; set; } // Task priority (e.g., Urgent, High)

        [Required]
        public DateTime DueDate { get; set; } // Due date of the task

        public int CommentsCount { get; set; } // Number of comments

        public List<string> Tags { get; set; } // Additional tags or labels related to the task
    }
}
