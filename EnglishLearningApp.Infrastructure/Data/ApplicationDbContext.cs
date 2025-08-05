using EnglishLearningApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EnglishLearningApp.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<UserProgress> UserProgress { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Lesson configuration
        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Theme).IsRequired().HasMaxLength(100);
            entity.Property(e => e.TaskType).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Difficulty).IsRequired();
            entity.Property(e => e.IsActive).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
        });

        // Exercise configuration
        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Question).IsRequired();
            entity.Property(e => e.CorrectAnswer).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Explanation).HasMaxLength(500);
            entity.Property(e => e.Order).IsRequired();
            
            // Configure Options as JSON
            entity.Property(e => e.Options)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>())
                .HasColumnType("nvarchar(max)");
            
            entity.HasOne(e => e.Lesson)
                .WithMany(l => l.Exercises)
                .HasForeignKey(e => e.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // UserProgress configuration
        modelBuilder.Entity<UserProgress>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserId).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Score).IsRequired();
            entity.Property(e => e.TotalQuestions).IsRequired();
            entity.Property(e => e.CompletedAt).IsRequired();
            entity.Property(e => e.IsCompleted).IsRequired();
            
            entity.HasOne(e => e.Lesson)
                .WithMany()
                .HasForeignKey(e => e.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Lessons
        var lessons = new[]
        {
            new Lesson
            {
                Id = 1,
                Title = "Pronouns Exercise",
                Description = "Learn and practice using different types of pronouns in English sentences.",
                Theme = "Pronouns",
                TaskType = "Fill in the blanks with an appropriate pronoun.",
                Difficulty = 1,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Lesson
            {
                Id = 2,
                Title = "Verb Tenses",
                Description = "Practice using different verb tenses in English.",
                Theme = "Grammar",
                TaskType = "Choose the correct verb form for each sentence.",
                Difficulty = 2,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Lesson
            {
                Id = 3,
                Title = "Articles Exercise",
                Description = "Learn when to use 'a', 'an', and 'the' in English sentences.",
                Theme = "Articles",
                TaskType = "Fill in the blanks with the correct article.",
                Difficulty = 1,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        };

        modelBuilder.Entity<Lesson>().HasData(lessons);

        // Seed Exercises for Pronouns Exercise (Lesson 1)
        var pronounsExercises = new[]
        {
            new Exercise
            {
                Id = 1,
                LessonId = 1,
                Question = "My mother wanted ............................ to become a doctor.",
                CorrectAnswer = "me",
                Options = new List<string> { "me", "I", "Either could be used here" },
                Explanation = "Use 'me' as the object of the verb 'wanted'.",
                Order = 1
            },
            new Exercise
            {
                Id = 2,
                LessonId = 1,
                Question = "............................ is going to the store.",
                CorrectAnswer = "She",
                Options = new List<string> { "She", "Her", "Hers" },
                Explanation = "Use 'She' as the subject of the sentence.",
                Order = 2
            },
            new Exercise
            {
                Id = 3,
                LessonId = 1,
                Question = "The book belongs to ............................ .",
                CorrectAnswer = "him",
                Options = new List<string> { "he", "him", "his" },
                Explanation = "Use 'him' as the object of the preposition 'to'.",
                Order = 3
            },
            new Exercise
            {
                Id = 4,
                LessonId = 1,
                Question = "............................ are going to the party.",
                CorrectAnswer = "They",
                Options = new List<string> { "Them", "They", "Their" },
                Explanation = "Use 'They' as the subject of the sentence.",
                Order = 4
            },
            new Exercise
            {
                Id = 5,
                LessonId = 1,
                Question = "This is ............................ car.",
                CorrectAnswer = "my",
                Options = new List<string> { "me", "my", "mine" },
                Explanation = "Use 'my' as a possessive adjective before the noun 'car'.",
                Order = 5
            }
        };

        // Seed Exercises for Verb Tenses (Lesson 2)
        var verbTensesExercises = new[]
        {
            new Exercise
            {
                Id = 6,
                LessonId = 2,
                Question = "I ............................ to the store yesterday.",
                CorrectAnswer = "went",
                Options = new List<string> { "go", "went", "gone", "going" },
                Explanation = "Use past simple tense for completed actions in the past.",
                Order = 1
            },
            new Exercise
            {
                Id = 7,
                LessonId = 2,
                Question = "She ............................ English for five years.",
                CorrectAnswer = "has been studying",
                Options = new List<string> { "studies", "studied", "has been studying", "will study" },
                Explanation = "Use present perfect continuous for actions that started in the past and continue to the present.",
                Order = 2
            },
            new Exercise
            {
                Id = 8,
                LessonId = 2,
                Question = "They ............................ dinner when I called.",
                CorrectAnswer = "were having",
                Options = new List<string> { "have", "had", "were having", "will have" },
                Explanation = "Use past continuous for actions in progress at a specific time in the past.",
                Order = 3
            },
            new Exercise
            {
                Id = 9,
                LessonId = 2,
                Question = "I ............................ to Paris next summer.",
                CorrectAnswer = "will go",
                Options = new List<string> { "go", "went", "will go", "am going" },
                Explanation = "Use future simple for planned future actions.",
                Order = 4
            },
            new Exercise
            {
                Id = 10,
                LessonId = 2,
                Question = "He ............................ in London since 2010.",
                CorrectAnswer = "has lived",
                Options = new List<string> { "lives", "lived", "has lived", "will live" },
                Explanation = "Use present perfect for actions that started in the past and continue to the present.",
                Order = 5
            }
        };

        // Seed Exercises for Articles Exercise (Lesson 3)
        var articlesExercises = new[]
        {
            new Exercise
            {
                Id = 11,
                LessonId = 3,
                Question = "I saw ............................ elephant at the zoo.",
                CorrectAnswer = "an",
                Options = new List<string> { "a", "an", "the", "no article" },
                Explanation = "Use 'an' before words that begin with a vowel sound.",
                Order = 1
            },
            new Exercise
            {
                Id = 12,
                LessonId = 3,
                Question = "............................ sun rises in the east.",
                CorrectAnswer = "The",
                Options = new List<string> { "A", "An", "The", "No article" },
                Explanation = "Use 'the' for unique objects or specific things.",
                Order = 2
            },
            new Exercise
            {
                Id = 13,
                LessonId = 3,
                Question = "She bought ............................ car last week.",
                CorrectAnswer = "a",
                Options = new List<string> { "a", "an", "the", "no article" },
                Explanation = "Use 'a' before words that begin with a consonant sound.",
                Order = 3
            },
            new Exercise
            {
                Id = 14,
                LessonId = 3,
                Question = "............................ United States is a large country.",
                CorrectAnswer = "The",
                Options = new List<string> { "A", "An", "The", "No article" },
                Explanation = "Use 'the' with country names that contain words like 'United', 'Kingdom', 'Republic'.",
                Order = 4
            },
            new Exercise
            {
                Id = 15,
                LessonId = 3,
                Question = "I need ............................ umbrella because it's raining.",
                CorrectAnswer = "an",
                Options = new List<string> { "a", "an", "the", "no article" },
                Explanation = "Use 'an' before words that begin with a vowel sound.",
                Order = 5
            }
        };

        modelBuilder.Entity<Exercise>().HasData(pronounsExercises);
        modelBuilder.Entity<Exercise>().HasData(verbTensesExercises);
        modelBuilder.Entity<Exercise>().HasData(articlesExercises);
    }
} 