namespace EnglishLearningApp.Application.DTOs;

public class LessonDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Theme { get; set; } = string.Empty;
    public string TaskType { get; set; } = string.Empty;
    public int Difficulty { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ExerciseCount { get; set; }
    public bool IsCompleted { get; set; }
    public int? UserScore { get; set; }
} 