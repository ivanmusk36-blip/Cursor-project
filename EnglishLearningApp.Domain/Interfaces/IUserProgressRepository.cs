using EnglishLearningApp.Domain.Entities;

namespace EnglishLearningApp.Domain.Interfaces;

public interface IUserProgressRepository
{
    Task<IEnumerable<UserProgress>> GetByUserIdAsync(string userId);
    Task<UserProgress?> GetByUserIdAndLessonIdAsync(string userId, int lessonId);
    Task<UserProgress> AddAsync(UserProgress userProgress);
    Task<UserProgress> UpdateAsync(UserProgress userProgress);
    Task DeleteAsync(int id);
} 