using EnglishLearningApp.Application.DTOs;
using EnglishLearningApp.Application.Interfaces;
using EnglishLearningApp.Domain.Entities;
using EnglishLearningApp.Domain.Interfaces;

namespace EnglishLearningApp.Application.Services;

public class LessonService : ILessonService
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IUserProgressRepository _userProgressRepository;

    public LessonService(ILessonRepository lessonRepository, IUserProgressRepository userProgressRepository)
    {
        _lessonRepository = lessonRepository;
        _userProgressRepository = userProgressRepository;
    }

    public async Task<IEnumerable<LessonDto>> GetAllLessonsAsync(string userId)
    {
        var lessons = await _lessonRepository.GetAllAsync();
        var userProgress = await _userProgressRepository.GetByUserIdAsync(userId);
        
        var lessonDtos = lessons.Select(lesson =>
        {
            var progress = userProgress.FirstOrDefault(p => p.LessonId == lesson.Id);
            return new LessonDto
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Description = lesson.Description,
                Theme = lesson.Theme,
                TaskType = lesson.TaskType,
                Difficulty = lesson.Difficulty,
                IsActive = lesson.IsActive,
                CreatedAt = lesson.CreatedAt,
                ExerciseCount = lesson.Exercises.Count,
                IsCompleted = progress?.IsCompleted ?? false,
                UserScore = progress?.Score
            };
        });

        return lessonDtos;
    }

    public async Task<LessonDto?> GetLessonByIdAsync(int id, string userId)
    {
        var lesson = await _lessonRepository.GetByIdAsync(id);
        if (lesson == null) return null;

        var progress = await _userProgressRepository.GetByUserIdAndLessonIdAsync(userId, id);
        
        return new LessonDto
        {
            Id = lesson.Id,
            Title = lesson.Title,
            Description = lesson.Description,
            Theme = lesson.Theme,
            TaskType = lesson.TaskType,
            Difficulty = lesson.Difficulty,
            IsActive = lesson.IsActive,
            CreatedAt = lesson.CreatedAt,
            ExerciseCount = lesson.Exercises.Count,
            IsCompleted = progress?.IsCompleted ?? false,
            UserScore = progress?.Score
        };
    }

    public async Task<LessonWithExercisesDto?> GetLessonWithExercisesAsync(int id)
    {
        var lesson = await _lessonRepository.GetByIdWithExercisesAsync(id);
        if (lesson == null) return null;

        return new LessonWithExercisesDto
        {
            Id = lesson.Id,
            Title = lesson.Title,
            Description = lesson.Description,
            Theme = lesson.Theme,
            TaskType = lesson.TaskType,
            Difficulty = lesson.Difficulty,
            Exercises = lesson.Exercises
                .OrderBy(e => e.Order)
                .Select(e => new ExerciseDto
                {
                    Id = e.Id,
                    Question = e.Question,
                    Options = e.Options,
                    Order = e.Order
                })
                .ToList()
        };
    }

    public async Task<UserProgressDto> SubmitLessonAnswersAsync(int lessonId, string userId, Dictionary<int, string> answers)
    {
        var lesson = await _lessonRepository.GetByIdWithExercisesAsync(lessonId);
        if (lesson == null)
            throw new ArgumentException("Lesson not found");

        var exercises = lesson.Exercises.OrderBy(e => e.Order).ToList();
        var totalQuestions = exercises.Count;
        
        if (totalQuestions == 0)
            throw new ArgumentException("No exercises found for this lesson");

        var correctAnswers = 0;

        foreach (var exercise in exercises)
        {
            if (answers.TryGetValue(exercise.Id, out var userAnswer) && 
                userAnswer.Equals(exercise.CorrectAnswer, StringComparison.OrdinalIgnoreCase))
            {
                correctAnswers++;
            }
        }

        // Calculate percentage score (0-100)
        var score = totalQuestions > 0 ? (int)((double)correctAnswers / totalQuestions * 100) : 0;
        
        // Ensure score is within valid range
        score = Math.Max(0, Math.Min(100, score));
        
        var progress = await _userProgressRepository.GetByUserIdAndLessonIdAsync(userId, lessonId);
        if (progress == null)
        {
            progress = new UserProgress
            {
                UserId = userId,
                LessonId = lessonId,
                Score = score,
                TotalQuestions = totalQuestions,
                IsCompleted = true,
                CompletedAt = DateTime.UtcNow
            };
            await _userProgressRepository.AddAsync(progress);
        }
        else
        {
            progress.Score = score;
            progress.TotalQuestions = totalQuestions;
            progress.IsCompleted = true;
            progress.CompletedAt = DateTime.UtcNow;
            await _userProgressRepository.UpdateAsync(progress);
        }

        return new UserProgressDto
        {
            Id = progress.Id,
            LessonId = lessonId,
            LessonTitle = lesson.Title,
            Score = score,
            TotalQuestions = totalQuestions,
            Percentage = (double)score / 100,
            CompletedAt = progress.CompletedAt,
            IsCompleted = true
        };
    }
} 