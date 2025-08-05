namespace EnglishLearningApp.Application.DTOs;

public class ExerciseDto
{
    public int Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new List<string>();
    public int Order { get; set; }
    public string? UserAnswer { get; set; }
    public bool IsCorrect { get; set; }
} 