using EnglishLearningApp.Application.DTOs;
using EnglishLearningApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearningApp.Presentation.Controllers;

public class LessonsController : Controller
{
    private readonly ILessonService _lessonService;
    private readonly IUserProgressService _userProgressService;
    private readonly IInfiniteTestService _infiniteTestService;
    private readonly ISavedExerciseService _savedExerciseService;

    public LessonsController(ILessonService lessonService, IUserProgressService userProgressService, IInfiniteTestService infiniteTestService, ISavedExerciseService savedExerciseService)
    {
        _lessonService = lessonService;
        _userProgressService = userProgressService;
        _infiniteTestService = infiniteTestService;
        _savedExerciseService = savedExerciseService;
    }

    public async Task<IActionResult> Index()
    {
        // For demo purposes, using a fixed user ID
        var userId = "demo-user";
        var lessons = await _lessonService.GetAllLessonsAsync(userId);
        return View(lessons);
    }

    public async Task<IActionResult> Details(int id)
    {
        // For demo purposes, using a fixed user ID
        var userId = "demo-user";
        var lesson = await _lessonService.GetLessonByIdAsync(id, userId);
        
        if (lesson == null)
            return NotFound();

        return View(lesson);
    }

    public async Task<IActionResult> Start(int id)
    {
        var lesson = await _lessonService.GetLessonWithExercisesAsync(id);
        
        if (lesson == null)
            return NotFound();

        return View(lesson);
    }

    [HttpPost]
    public async Task<IActionResult> Submit(int id, Dictionary<int, string> answers)
    {
        // For demo purposes, using a fixed user ID
        var userId = "demo-user";
        
        try
        {
            var result = await _lessonService.SubmitLessonAnswersAsync(id, userId, answers);
            return RedirectToAction(nameof(Result), new { lessonId = id });
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    public async Task<IActionResult> Result(int lessonId)
    {
        // For demo purposes, using a fixed user ID
        var userId = "demo-user";
        var progress = await _userProgressService.GetUserProgressForLessonAsync(userId, lessonId);
        
        if (progress == null)
            return NotFound();

        return View(progress);
    }

    // Infinite Test Actions
    public async Task<IActionResult> InfiniteTest()
    {
        var infiniteTest = await _infiniteTestService.GenerateInfiniteTestAsync();
        return View(infiniteTest);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitInfiniteTest(Dictionary<int, string> answers, string generatedRule, bool saveExercise = true)
    {
        // For demo purposes, using a fixed user ID
        var userId = "demo-user";
        
        try
        {
            var result = await _infiniteTestService.SubmitInfiniteTestAnswersAsync(userId, answers, generatedRule);
            
            // Save the exercise if requested
            if (saveExercise)
            {
                // Get the current test from session or regenerate it
                var currentTest = await _infiniteTestService.GenerateInfiniteTestAsync();
                
                var saveRequest = new SaveExerciseRequestDto
                {
                    UserId = userId,
                    Exercise = currentTest,
                    UserAnswers = answers,
                    Score = result.Score,
                    TotalQuestions = result.TotalQuestions,
                    Percentage = result.Percentage
                };
                
                await _savedExerciseService.SaveExerciseAsync(saveRequest);
            }
            
            return RedirectToAction(nameof(InfiniteTestResult), new { generatedRule = generatedRule });
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    public IActionResult InfiniteTestResult(string generatedRule)
    {
        // Since infinite tests don't save to database, we'll create a mock result
        var mockResult = new UserProgressDto
        {
            Id = 0,
            LessonId = -1,
            LessonTitle = $"Infinite Test - {generatedRule}",
            Score = new Random().Next(60, 95), // Random score for demo
            TotalQuestions = 10,
            Percentage = 0.75, // Mock percentage
            CompletedAt = DateTime.UtcNow,
            IsCompleted = true
        };

        return View(mockResult);
    }
} 