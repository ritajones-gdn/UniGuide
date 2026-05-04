using System.ComponentModel.DataAnnotations;

namespace UniGuide.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<Answer> Answers { get; set; } = new List<Answer>();
    }
}
