using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EnglishLearningApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSavedExercises : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Theme = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TaskType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Difficulty = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SavedExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Theme = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TaskType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Difficulty = table.Column<int>(type: "int", nullable: false),
                    GeneratedRule = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: true),
                    TotalQuestions = table.Column<int>(type: "int", nullable: true),
                    Percentage = table.Column<double>(type: "float", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedExercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Options = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProgress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    TotalQuestions = table.Column<int>(type: "int", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProgress_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavedExerciseQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SavedExerciseId = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OptionsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    UserAnswer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedExerciseQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedExerciseQuestions_SavedExercises_SavedExerciseId",
                        column: x => x.SavedExerciseId,
                        principalTable: "SavedExercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "CreatedAt", "Description", "Difficulty", "IsActive", "TaskType", "Theme", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 5, 13, 8, 1, 129, DateTimeKind.Utc).AddTicks(6976), "Learn and practice using different types of pronouns in English sentences.", 1, true, "Fill in the blanks with an appropriate pronoun.", "Pronouns", "Pronouns Exercise" },
                    { 2, new DateTime(2025, 8, 5, 13, 8, 1, 129, DateTimeKind.Utc).AddTicks(6978), "Practice using different verb tenses in English.", 2, true, "Choose the correct verb form for each sentence.", "Grammar", "Verb Tenses" },
                    { 3, new DateTime(2025, 8, 5, 13, 8, 1, 129, DateTimeKind.Utc).AddTicks(6980), "Learn when to use 'a', 'an', and 'the' in English sentences.", 1, true, "Fill in the blanks with the correct article.", "Articles", "Articles Exercise" }
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "CorrectAnswer", "Explanation", "LessonId", "Options", "Order", "Question" },
                values: new object[,]
                {
                    { 1, "me", "Use 'me' as the object of the verb 'wanted'.", 1, "[\"me\",\"I\",\"Either could be used here\"]", 1, "My mother wanted ............................ to become a doctor." },
                    { 2, "She", "Use 'She' as the subject of the sentence.", 1, "[\"She\",\"Her\",\"Hers\"]", 2, "............................ is going to the store." },
                    { 3, "him", "Use 'him' as the object of the preposition 'to'.", 1, "[\"he\",\"him\",\"his\"]", 3, "The book belongs to ............................ ." },
                    { 4, "They", "Use 'They' as the subject of the sentence.", 1, "[\"Them\",\"They\",\"Their\"]", 4, "............................ are going to the party." },
                    { 5, "my", "Use 'my' as a possessive adjective before the noun 'car'.", 1, "[\"me\",\"my\",\"mine\"]", 5, "This is ............................ car." },
                    { 6, "went", "Use past simple tense for completed actions in the past.", 2, "[\"go\",\"went\",\"gone\",\"going\"]", 1, "I ............................ to the store yesterday." },
                    { 7, "has been studying", "Use present perfect continuous for actions that started in the past and continue to the present.", 2, "[\"studies\",\"studied\",\"has been studying\",\"will study\"]", 2, "She ............................ English for five years." },
                    { 8, "were having", "Use past continuous for actions in progress at a specific time in the past.", 2, "[\"have\",\"had\",\"were having\",\"will have\"]", 3, "They ............................ dinner when I called." },
                    { 9, "will go", "Use future simple for planned future actions.", 2, "[\"go\",\"went\",\"will go\",\"am going\"]", 4, "I ............................ to Paris next summer." },
                    { 10, "has lived", "Use present perfect for actions that started in the past and continue to the present.", 2, "[\"lives\",\"lived\",\"has lived\",\"will live\"]", 5, "He ............................ in London since 2010." },
                    { 11, "an", "Use 'an' before words that begin with a vowel sound.", 3, "[\"a\",\"an\",\"the\",\"no article\"]", 1, "I saw ............................ elephant at the zoo." },
                    { 12, "The", "Use 'the' for unique objects or specific things.", 3, "[\"A\",\"An\",\"The\",\"No article\"]", 2, "............................ sun rises in the east." },
                    { 13, "a", "Use 'a' before words that begin with a consonant sound.", 3, "[\"a\",\"an\",\"the\",\"no article\"]", 3, "She bought ............................ car last week." },
                    { 14, "The", "Use 'the' with country names that contain words like 'United', 'Kingdom', 'Republic'.", 3, "[\"A\",\"An\",\"The\",\"No article\"]", 4, "............................ United States is a large country." },
                    { 15, "an", "Use 'an' before words that begin with a vowel sound.", 3, "[\"a\",\"an\",\"the\",\"no article\"]", 5, "I need ............................ umbrella because it's raining." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_LessonId",
                table: "Exercises",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedExerciseQuestions_SavedExerciseId",
                table: "SavedExerciseQuestions",
                column: "SavedExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProgress_LessonId",
                table: "UserProgress",
                column: "LessonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "SavedExerciseQuestions");

            migrationBuilder.DropTable(
                name: "UserProgress");

            migrationBuilder.DropTable(
                name: "SavedExercises");

            migrationBuilder.DropTable(
                name: "Lessons");
        }
    }
}
