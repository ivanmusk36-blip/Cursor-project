using EnglishLearningApp.Domain.Entities;
using EnglishLearningApp.Domain.Interfaces;
using EnglishLearningApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningApp.Infrastructure.Repositories;

public class SavedExerciseRepository : ISavedExerciseRepository
{
    private readonly ApplicationDbContext _context;

    public SavedExerciseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SavedExercise?> GetByIdAsync(int id)
    {
        return await _context.SavedExercises
            .Include(se => se.Questions)
            .FirstOrDefaultAsync(se => se.Id == id);
    }

    public async Task<List<SavedExercise>> GetByUserIdAsync(string userId)
    {
        return await _context.SavedExercises
            .Include(se => se.Questions)
            .Where(se => se.UserId == userId)
            .OrderByDescending(se => se.CreatedAt)
            .ToListAsync();
    }

    public async Task<SavedExercise> SaveAsync(SavedExercise savedExercise)
    {
        if (savedExercise.Id == 0)
        {
            _context.SavedExercises.Add(savedExercise);
        }
        else
        {
            _context.SavedExercises.Update(savedExercise);
        }

        await _context.SaveChangesAsync();
        return savedExercise;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exercise = await _context.SavedExercises.FindAsync(id);
        if (exercise == null)
            return false;

        _context.SavedExercises.Remove(exercise);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<SavedExercise>> GetCompletedByUserIdAsync(string userId)
    {
        return await _context.SavedExercises
            .Include(se => se.Questions)
            .Where(se => se.UserId == userId && se.IsCompleted)
            .OrderByDescending(se => se.CompletedAt)
            .ToListAsync();
    }

    public async Task<List<SavedExercise>> GetInProgressByUserIdAsync(string userId)
    {
        return await _context.SavedExercises
            .Include(se => se.Questions)
            .Where(se => se.UserId == userId && !se.IsCompleted)
            .OrderByDescending(se => se.CreatedAt)
            .ToListAsync();
    }
} 