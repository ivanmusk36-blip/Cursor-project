using EnglishLearningApp.Application.DTOs;

namespace EnglishLearningApp.Application.Interfaces;

public interface ILessonService
{
    Task<IEnumerable<LessonDto>> GetAllLessonsAsync(string userId);
    Task<LessonDto?> GetLessonByIdAsync(int id, string userId);
    Task<LessonWithExercisesDto?> GetLessonWithExercisesAsync(int id);
    Task<UserProgressDto> SubmitLessonAnswersAsync(int lessonId, string userId, Dictionary<int, string> answers);
} 