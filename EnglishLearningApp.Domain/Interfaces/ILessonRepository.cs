using EnglishLearningApp.Domain.Entities;

namespace EnglishLearningApp.Domain.Interfaces;

public interface ILessonRepository
{
    Task<IEnumerable<Lesson>> GetAllAsync();
    Task<Lesson?> GetByIdAsync(int id);
    Task<Lesson?> GetByIdWithExercisesAsync(int id);
    Task<Lesson> AddAsync(Lesson lesson);
    Task<Lesson> UpdateAsync(Lesson lesson);
    Task DeleteAsync(int id);
} 