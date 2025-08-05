using EnglishLearningApp.Application.DTOs;
using EnglishLearningApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearningApp.Presentation.Controllers;

public class LessonsController : Controller
{
    private readonly ILessonService _lessonService;
    private readonly IUserProgressService _userProgressService;

    public LessonsController(ILessonService lessonService, IUserProgressService userProgressService)
    {
        _lessonService = lessonService;
        _userProgressService = userProgressService;
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
} 