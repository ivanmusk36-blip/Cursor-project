using EnglishLearningApp.Application.DTOs;

namespace EnglishLearningApp.Application.Interfaces;

public interface IInfiniteTestService
{
    Task<InfiniteTestDto> GenerateInfiniteTestAsync();
    Task<UserProgressDto> SubmitInfiniteTestAnswersAsync(string userId, Dictionary<int, string> answers, string generatedRule);
} 