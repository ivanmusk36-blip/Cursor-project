namespace EnglishLearningApp.Domain.Entities;

public class SavedExercise
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Theme { get; set; } = string.Empty;
    public string TaskType { get; set; } = string.Empty;
    public int Difficulty { get; set; }
    public string? GeneratedRule { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int? Score { get; set; }
    public int? TotalQuestions { get; set; }
    public double? Percentage { get; set; }
    public bool IsCompleted { get; set; }
    
    // Navigation property
    public List<SavedExerciseQuestion> Questions { get; set; } = new List<SavedExerciseQuestion>();
}

public class SavedExerciseQuestion
{
    public int Id { get; set; }
    public int SavedExerciseId { get; set; }
    public string Question { get; set; } = string.Empty;
    public string CorrectAnswer { get; set; } = string.Empty;
    public string OptionsJson { get; set; } = string.Empty; // Store as JSON
    public string Explanation { get; set; } = string.Empty;
    public int Order { get; set; }
    public string? UserAnswer { get; set; }
    public bool IsCorrect { get; set; }
    
    // Navigation property
    public SavedExercise SavedExercise { get; set; } = null!;
} 