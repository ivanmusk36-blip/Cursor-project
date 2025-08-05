# English Learning App

A .NET 8 MVC application designed to help people learn English through interactive exercises and lessons.

## Features

- **Interactive Lessons**: Multiple-choice exercises with immediate feedback
- **Progress Tracking**: Track your scores and completion status
- **Beautiful UI**: Modern, responsive design with Bootstrap 5
- **Clean Architecture**: Follows Clean Architecture principles with proper separation of concerns

## Architecture

The application follows Clean Architecture principles:

- **Domain Layer**: Contains entities and interfaces
- **Application Layer**: Contains business logic and DTOs
- **Infrastructure Layer**: Contains data access and external services
- **Presentation Layer**: Contains controllers and views

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server LocalDB (included with Visual Studio)

### Running the Application

1. Clone the repository
2. Navigate to the project directory
3. Run the following commands:

```bash
dotnet restore
dotnet build
cd EnglishLearningApp.Presentation
dotnet run
```

4. Open your browser and navigate to `https://localhost:5001` or `http://localhost:5000`

## Available Lessons

1. **Pronouns Exercise**: Learn and practice using different types of pronouns
2. **Verb Tenses**: Practice using different verb tenses in English
3. **Articles Exercise**: Learn when to use 'a', 'an', and 'the'

## How to Use

1. **Browse Lessons**: View all available lessons on the main page
2. **Start a Lesson**: Click "Start Lesson" to begin an exercise
3. **Answer Questions**: Select the correct answer for each question
4. **Submit Answers**: Click "Submit Answers" to see your results
5. **View Results**: See your score and performance feedback
6. **Retake Lessons**: Improve your score by retaking lessons

## Technology Stack

- **.NET 8**: Latest version of .NET
- **ASP.NET Core MVC**: Web framework
- **Entity Framework Core**: ORM for data access
- **SQL Server LocalDB**: Local database
- **Bootstrap 5**: CSS framework for responsive design
- **Bootstrap Icons**: Icon library

## Project Structure

```
EnglishLearningApp/
├── EnglishLearningApp.Domain/          # Domain entities and interfaces
├── EnglishLearningApp.Application/     # Business logic and DTOs
├── EnglishLearningApp.Infrastructure/  # Data access and external services
└── EnglishLearningApp.Presentation/    # Controllers and views
```

## Database

The application uses Entity Framework Core with SQL Server LocalDB. The database is automatically created when the application starts for the first time.

## Contributing

This is a demo application showcasing Clean Architecture principles in .NET 8. Feel free to extend it with additional features like:

- User authentication
- More lesson types
- Advanced progress tracking
- Admin panel for managing lessons
- Export functionality for progress reports 