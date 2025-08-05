using EnglishLearningApp.Domain.Entities;
using EnglishLearningApp.Domain.Interfaces;
using EnglishLearningApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningApp.Infrastructure.Repositories;

public class UserProgressRepository : IUserProgressRepository
{
    private readonly ApplicationDbContext _context;

    public UserProgressRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserProgress>> GetByUserIdAsync(string userId)
    {
        return await _context.UserProgress
            .Where(up => up.UserId == userId)
            .OrderByDescending(up => up.CompletedAt)
            .ToListAsync();
    }

    public async Task<UserProgress?> GetByUserIdAndLessonIdAsync(string userId, int lessonId)
    {
        return await _context.UserProgress
            .FirstOrDefaultAsync(up => up.UserId == userId && up.LessonId == lessonId);
    }

    public async Task<UserProgress> AddAsync(UserProgress userProgress)
    {
        _context.UserProgress.Add(userProgress);
        await _context.SaveChangesAsync();
        return userProgress;
    }

    public async Task<UserProgress> UpdateAsync(UserProgress userProgress)
    {
        _context.UserProgress.Update(userProgress);
        await _context.SaveChangesAsync();
        return userProgress;
    }

    public async Task DeleteAsync(int id)
    {
        var userProgress = await _context.UserProgress.FindAsync(id);
        if (userProgress != null)
        {
            _context.UserProgress.Remove(userProgress);
            await _context.SaveChangesAsync();
        }
    }
} 