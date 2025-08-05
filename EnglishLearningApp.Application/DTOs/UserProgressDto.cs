namespace EnglishLearningApp.Application.DTOs;

public class UserProgressDto
{
    public int Id { get; set; }
    public int LessonId { get; set; }
    public string LessonTitle { get; set; } = string.Empty;
    public int Score { get; set; }
    public int TotalQuestions { get; set; }
    public double Percentage { get; set; }
    public DateTime CompletedAt { get; set; }
    public bool IsCompleted { get; set; }
} 