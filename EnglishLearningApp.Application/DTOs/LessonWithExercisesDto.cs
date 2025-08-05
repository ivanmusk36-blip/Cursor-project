namespace EnglishLearningApp.Application.DTOs;

public class LessonWithExercisesDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Theme { get; set; } = string.Empty;
    public string TaskType { get; set; } = string.Empty;
    public int Difficulty { get; set; }
    public List<ExerciseDto> Exercises { get; set; } = new List<ExerciseDto>();
} 