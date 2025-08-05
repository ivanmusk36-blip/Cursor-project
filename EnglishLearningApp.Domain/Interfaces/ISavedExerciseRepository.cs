using EnglishLearningApp.Domain.Entities;

namespace EnglishLearningApp.Domain.Interfaces;

public interface ISavedExerciseRepository
{
    Task<SavedExercise?> GetByIdAsync(int id);
    Task<List<SavedExercise>> GetByUserIdAsync(string userId);
    Task<SavedExercise> SaveAsync(SavedExercise savedExercise);
    Task<bool> DeleteAsync(int id);
    Task<List<SavedExercise>> GetCompletedByUserIdAsync(string userId);
    Task<List<SavedExercise>> GetInProgressByUserIdAsync(string userId);
} 