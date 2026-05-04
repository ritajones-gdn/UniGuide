using System.ComponentModel.DataAnnotations;

namespace UniGuide.Models
{
    public class Answer
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public int QuestionId { get; set; }

        public Question? Question { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Major { get; set; } = string.Empty;

        public int Likes { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
