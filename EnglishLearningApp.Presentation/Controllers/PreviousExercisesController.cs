using EnglishLearningApp.Application.DTOs;
using EnglishLearningApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearningApp.Presentation.Controllers;

public class PreviousExercisesController : Controller
{
    private readonly ISavedExerciseService _savedExerciseService;

    public PreviousExercisesController(ISavedExerciseService savedExerciseService)
    {
        _savedExerciseService = savedExerciseService;
    }

    public async Task<IActionResult> Index()
    {
        // For demo purposes, using a fixed user ID
        var userId = "demo-user";
        var exercises = await _savedExerciseService.GetUserExercisesAsync(userId);
        
        return View(exercises);
    }

    public async Task<IActionResult> Details(int id)
    {
        var exercise = await _savedExerciseService.GetExerciseByIdAsync(id);
        if (exercise == null)
        {
            return NotFound();
        }

        return View(exercise);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _savedExerciseService.DeleteExerciseAsync(id);
        if (!success)
        {
            return NotFound();
        }

        TempData["Message"] = "Exercise deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Completed()
    {
        var userId = "demo-user";
        var exercises = await _savedExerciseService.GetCompletedExercisesAsync(userId);
        
        return View("Index", exercises);
    }

    public async Task<IActionResult> InProgress()
    {
        var userId = "demo-user";
        var exercises = await _savedExerciseService.GetInProgressExercisesAsync(userId);
        
        return View("Index", exercises);
    }
} 