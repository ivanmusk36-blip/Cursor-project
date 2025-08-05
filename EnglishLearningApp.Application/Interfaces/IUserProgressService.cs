using EnglishLearningApp.Application.DTOs;

namespace EnglishLearningApp.Application.Interfaces;

public interface IUserProgressService
{
    Task<IEnumerable<UserProgressDto>> GetUserProgressAsync(string userId);
    Task<UserProgressDto?> GetUserProgressForLessonAsync(string userId, int lessonId);
    Task<UserProgressDto> SaveUserProgressAsync(string userId, int lessonId, int score, int totalQuestions);
} 