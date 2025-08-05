using System.Text.Json.Serialization;

namespace EnglishLearningApp.Application.DTOs;

public class SavedExerciseDto
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
    public List<SavedExerciseQuestionDto> Questions { get; set; } = new List<SavedExerciseQuestionDto>();
}

public class SavedExerciseQuestionDto
{
    public int Id { get; set; }
    public int SavedExerciseId { get; set; }
    public string Question { get; set; } = string.Empty;
    public string CorrectAnswer { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new List<string>();
    public string Explanation { get; set; } = string.Empty;
    public int Order { get; set; }
    public string? UserAnswer { get; set; }
    public bool IsCorrect { get; set; }
}

public class SaveExerciseRequestDto
{
    public string UserId { get; set; } = string.Empty;
    public InfiniteTestDto Exercise { get; set; } = new InfiniteTestDto();
    public Dictionary<int, string> UserAnswers { get; set; } = new Dictionary<int, string>();
    public int Score { get; set; }
    public int TotalQuestions { get; set; }
    public double Percentage { get; set; }
} 