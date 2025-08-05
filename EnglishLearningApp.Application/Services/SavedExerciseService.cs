using EnglishLearningApp.Application.DTOs;
using EnglishLearningApp.Application.Interfaces;
using EnglishLearningApp.Domain.Entities;
using EnglishLearningApp.Domain.Interfaces;
using System.Text.Json;

namespace EnglishLearningApp.Application.Services;

public class SavedExerciseService : ISavedExerciseService
{
    private readonly ISavedExerciseRepository _savedExerciseRepository;

    public SavedExerciseService(ISavedExerciseRepository savedExerciseRepository)
    {
        _savedExerciseRepository = savedExerciseRepository;
    }

    public async Task<SavedExerciseDto> SaveExerciseAsync(SaveExerciseRequestDto request)
    {
        var savedExercise = new SavedExercise
        {
            UserId = request.UserId,
            Title = request.Exercise.Title,
            Description = request.Exercise.Description,
            Theme = request.Exercise.Theme,
            TaskType = request.Exercise.TaskType,
            Difficulty = request.Exercise.Difficulty,
            GeneratedRule = request.Exercise.GeneratedRule,
            CreatedAt = DateTime.UtcNow,
            CompletedAt = DateTime.UtcNow,
            Score = request.Score,
            TotalQuestions = request.TotalQuestions,
            Percentage = request.Percentage,
            IsCompleted = true,
            Questions = request.Exercise.Exercises.Select((ex, index) => new SavedExerciseQuestion
            {
                Question = ex.Question,
                CorrectAnswer = "", // We don't have correct answers for infinite tests
                OptionsJson = JsonSerializer.Serialize(ex.Options),
                Explanation = "",
                Order = ex.Order,
                UserAnswer = request.UserAnswers.TryGetValue(ex.Id, out var answer) ? answer : null,
                IsCorrect = false // We can't determine correctness without correct answers
            }).ToList()
        };

        var saved = await _savedExerciseRepository.SaveAsync(savedExercise);
        return MapToDto(saved);
    }

    public async Task<List<SavedExerciseDto>> GetUserExercisesAsync(string userId)
    {
        var exercises = await _savedExerciseRepository.GetByUserIdAsync(userId);
        return exercises.Select(MapToDto).ToList();
    }

    public async Task<SavedExerciseDto?> GetExerciseByIdAsync(int id)
    {
        var exercise = await _savedExerciseRepository.GetByIdAsync(id);
        return exercise != null ? MapToDto(exercise) : null;
    }

    public async Task<bool> DeleteExerciseAsync(int id)
    {
        return await _savedExerciseRepository.DeleteAsync(id);
    }

    public async Task<List<SavedExerciseDto>> GetCompletedExercisesAsync(string userId)
    {
        var exercises = await _savedExerciseRepository.GetCompletedByUserIdAsync(userId);
        return exercises.Select(MapToDto).ToList();
    }

    public async Task<List<SavedExerciseDto>> GetInProgressExercisesAsync(string userId)
    {
        var exercises = await _savedExerciseRepository.GetInProgressByUserIdAsync(userId);
        return exercises.Select(MapToDto).ToList();
    }

    private SavedExerciseDto MapToDto(SavedExercise entity)
    {
        return new SavedExerciseDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            Title = entity.Title,
            Description = entity.Description,
            Theme = entity.Theme,
            TaskType = entity.TaskType,
            Difficulty = entity.Difficulty,
            GeneratedRule = entity.GeneratedRule,
            CreatedAt = entity.CreatedAt,
            CompletedAt = entity.CompletedAt,
            Score = entity.Score,
            TotalQuestions = entity.TotalQuestions,
            Percentage = entity.Percentage,
            IsCompleted = entity.IsCompleted,
            Questions = entity.Questions.Select(q => new SavedExerciseQuestionDto
            {
                Id = q.Id,
                SavedExerciseId = q.SavedExerciseId,
                Question = q.Question,
                CorrectAnswer = q.CorrectAnswer,
                Options = JsonSerializer.Deserialize<List<string>>(q.OptionsJson) ?? new List<string>(),
                Explanation = q.Explanation,
                Order = q.Order,
                UserAnswer = q.UserAnswer,
                IsCorrect = q.IsCorrect
            }).ToList()
        };
    }
} 