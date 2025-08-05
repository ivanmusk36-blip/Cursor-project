using EnglishLearningApp.Application.DTOs;
using EnglishLearningApp.Application.Interfaces;
using EnglishLearningApp.Domain.Entities;
using EnglishLearningApp.Domain.Interfaces;

namespace EnglishLearningApp.Application.Services;

public class UserProgressService : IUserProgressService
{
    private readonly IUserProgressRepository _userProgressRepository;
    private readonly ILessonRepository _lessonRepository;

    public UserProgressService(IUserProgressRepository userProgressRepository, ILessonRepository lessonRepository)
    {
        _userProgressRepository = userProgressRepository;
        _lessonRepository = lessonRepository;
    }

    public async Task<IEnumerable<UserProgressDto>> GetUserProgressAsync(string userId)
    {
        var userProgress = await _userProgressRepository.GetByUserIdAsync(userId);
        var progressDtos = new List<UserProgressDto>();

        foreach (var progress in userProgress)
        {
            var lesson = await _lessonRepository.GetByIdAsync(progress.LessonId);
            if (lesson != null)
            {
                // Ensure score is within valid range
                var validScore = Math.Max(0, Math.Min(100, progress.Score));
                
                progressDtos.Add(new UserProgressDto
                {
                    Id = progress.Id,
                    LessonId = progress.LessonId,
                    LessonTitle = lesson.Title,
                    Score = validScore,
                    TotalQuestions = progress.TotalQuestions,
                    Percentage = (double)validScore / 100,
                    CompletedAt = progress.CompletedAt,
                    IsCompleted = progress.IsCompleted
                });
            }
        }

        return progressDtos;
    }

    public async Task<UserProgressDto?> GetUserProgressForLessonAsync(string userId, int lessonId)
    {
        var progress = await _userProgressRepository.GetByUserIdAndLessonIdAsync(userId, lessonId);
        if (progress == null) return null;

        var lesson = await _lessonRepository.GetByIdAsync(lessonId);
        if (lesson == null) return null;

        // Ensure score is within valid range
        var validScore = Math.Max(0, Math.Min(100, progress.Score));

        return new UserProgressDto
        {
            Id = progress.Id,
            LessonId = progress.LessonId,
            LessonTitle = lesson.Title,
            Score = validScore,
            TotalQuestions = progress.TotalQuestions,
            Percentage = (double)validScore / 100,
            CompletedAt = progress.CompletedAt,
            IsCompleted = progress.IsCompleted
        };
    }

    public async Task<UserProgressDto> SaveUserProgressAsync(string userId, int lessonId, int score, int totalQuestions)
    {
        // Ensure score is within valid range
        var validScore = Math.Max(0, Math.Min(100, score));
        
        var progress = await _userProgressRepository.GetByUserIdAndLessonIdAsync(userId, lessonId);
        
        if (progress == null)
        {
            progress = new UserProgress
            {
                UserId = userId,
                LessonId = lessonId,
                Score = validScore,
                TotalQuestions = totalQuestions,
                IsCompleted = true,
                CompletedAt = DateTime.UtcNow
            };
            await _userProgressRepository.AddAsync(progress);
        }
        else
        {
            progress.Score = validScore;
            progress.TotalQuestions = totalQuestions;
            progress.IsCompleted = true;
            progress.CompletedAt = DateTime.UtcNow;
            await _userProgressRepository.UpdateAsync(progress);
        }

        var lesson = await _lessonRepository.GetByIdAsync(lessonId);
        
        return new UserProgressDto
        {
            Id = progress.Id,
            LessonId = lessonId,
            LessonTitle = lesson?.Title ?? "Unknown Lesson",
            Score = validScore,
            TotalQuestions = totalQuestions,
            Percentage = (double)validScore / 100,
            CompletedAt = progress.CompletedAt,
            IsCompleted = true
        };
    }
} 