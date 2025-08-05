namespace EnglishLearningApp.Domain.Entities;

public class Exercise
{
    public int Id { get; set; }
    public int LessonId { get; set; }
    public string Question { get; set; } = string.Empty;
    public string CorrectAnswer { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new List<string>();
    public string Explanation { get; set; } = string.Empty;
    public int Order { get; set; }
    
    public virtual Lesson Lesson { get; set; } = null!;
} 