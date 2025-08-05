using EnglishLearningApp.Domain.Entities;
using EnglishLearningApp.Domain.Interfaces;
using EnglishLearningApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningApp.Infrastructure.Repositories;

public class LessonRepository : ILessonRepository
{
    private readonly ApplicationDbContext _context;

    public LessonRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Lesson>> GetAllAsync()
    {
        return await _context.Lessons
            .Where(l => l.IsActive)
            .OrderBy(l => l.Difficulty)
            .ThenBy(l => l.Title)
            .ToListAsync();
    }

    public async Task<Lesson?> GetByIdAsync(int id)
    {
        return await _context.Lessons
            .FirstOrDefaultAsync(l => l.Id == id && l.IsActive);
    }

    public async Task<Lesson?> GetByIdWithExercisesAsync(int id)
    {
        return await _context.Lessons
            .Include(l => l.Exercises)
            .FirstOrDefaultAsync(l => l.Id == id && l.IsActive);
    }

    public async Task<Lesson> AddAsync(Lesson lesson)
    {
        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();
        return lesson;
    }

    public async Task<Lesson> UpdateAsync(Lesson lesson)
    {
        _context.Lessons.Update(lesson);
        await _context.SaveChangesAsync();
        return lesson;
    }

    public async Task DeleteAsync(int id)
    {
        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson != null)
        {
            lesson.IsActive = false;
            await _context.SaveChangesAsync();
        }
    }
} 