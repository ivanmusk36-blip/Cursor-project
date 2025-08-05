namespace EnglishLearningApp.Domain.Entities;

public class UserProgress
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int LessonId { get; set; }
    public int Score { get; set; }
    public int TotalQuestions { get; set; }
    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
    public bool IsCompleted { get; set; }
    
    public virtual Lesson Lesson { get; set; } = null!;
} 