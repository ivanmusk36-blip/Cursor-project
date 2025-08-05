using System.Text.Json.Serialization;

namespace EnglishLearningApp.Application.DTOs;

public class InfiniteTestDto
{
    public string Title { get; set; } = "Infinite Test";
    public string Description { get; set; } = "AI-generated English grammar test with random questions";
    public string Theme { get; set; } = "Mixed Grammar";
    public string TaskType { get; set; } = "Multiple Choice";
    public int Difficulty { get; set; } = 2;
    public List<ExerciseDto> Exercises { get; set; } = new List<ExerciseDto>();
    public string? GeneratedRule { get; set; }
}

public class GrokTestRequest
{
    public string Rule { get; set; } = string.Empty;
    public int QuestionCount { get; set; } = 10;
}

public class GrokTestResponse
{
    [JsonPropertyName("rule")]
    public string Rule { get; set; } = string.Empty;
    
    [JsonPropertyName("questions")]
    public List<GrokQuestion> Questions { get; set; } = new List<GrokQuestion>();
}

public class GrokQuestion
{
    [JsonPropertyName("question")]
    public string Question { get; set; } = string.Empty;
    
    [JsonPropertyName("correctAnswer")]
    public string CorrectAnswer { get; set; } = string.Empty;
    
    [JsonPropertyName("options")]
    public List<string> Options { get; set; } = new List<string>();
    
    [JsonPropertyName("explanation")]
    public string Explanation { get; set; } = string.Empty;
} 