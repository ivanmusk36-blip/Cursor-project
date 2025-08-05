using EnglishLearningApp.Application.DTOs;

namespace EnglishLearningApp.Application.Interfaces;

public interface ISavedExerciseService
{
    Task<SavedExerciseDto> SaveExerciseAsync(SaveExerciseRequestDto request);
    Task<List<SavedExerciseDto>> GetUserExercisesAsync(string userId);
    Task<SavedExerciseDto?> GetExerciseByIdAsync(int id);
    Task<bool> DeleteExerciseAsync(int id);
    Task<List<SavedExerciseDto>> GetCompletedExercisesAsync(string userId);
    Task<List<SavedExerciseDto>> GetInProgressExercisesAsync(string userId);
} 