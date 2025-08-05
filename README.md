# English Learning App

A .NET 8 web application for learning English grammar through interactive lessons and AI-generated tests.

## Features

### Regular Lessons

- **Articles Exercise**: Learn when to use 'a', 'an', and 'the' in English sentences
- **Pronouns Exercise**: Practice using different types of pronouns in English sentences
- **Verb Tenses**: Practice using different verb tenses in English

### Infinite Test (NEW!)

- **AI-Generated Tests**: Uses Grok API to generate random English grammar tests
- **Random Rules**: Each test focuses on a random grammar rule (articles, pronouns, verb tenses, etc.)
- **10 Questions**: Each test contains 10 multiple-choice questions
- **No Progress Tracking**: Infinite tests don't save progress to the database, allowing unlimited practice

## Architecture

This application follows Clean Architecture principles:

- **Domain Layer**: Contains entities and interfaces
- **Application Layer**: Contains business logic and DTOs
- **Infrastructure Layer**: Contains data access and external service implementations
- **Presentation Layer**: Contains controllers and views

## Setup

1. **Clone the repository**
2. **Configure the API Key**:
   - The API key is stored securely using .NET User Secrets
   - Set your Grok API key using the following command:
   ```bash
   cd EnglishLearningApp.Presentation
   dotnet user-secrets set "testGrokAPIKey" "your-actual-grok-api-key-here"
   ```
3. **Run the application**:
   ```bash
   cd EnglishLearningApp.Presentation
   dotnet run
   ```

## Infinite Test Implementation

The infinite test feature uses the Grok API to generate random English grammar tests:

### API Integration

- **Endpoint**: `https://api.x.ai/v1/chat/completions`
- **Model**: `grok-beta`
- **Prompt**: Simple prompt requesting 10 multiple-choice questions about a random grammar rule
- **Fallback**: If API fails, uses hardcoded questions

### Random Rules

The system randomly selects from these grammar topics:

- Articles (a, an, the)
- Pronouns (he, she, they, etc.)
- Verb tenses (present, past, future)
- Prepositions (in, on, at, etc.)
- Adjectives and adverbs
- Conditional sentences
- Passive voice
- Reported speech
- Modal verbs
- Phrasal verbs

### Simple Prompt

The prompt is kept as simple as possible:

```
Generate 10 multiple choice English grammar questions about {randomRule}.
Format as JSON with: rule, questions array with question, correctAnswer, options array, explanation.
Make questions clear and explanations helpful.
```

### Security

- **API Key Security**: The Grok API key is stored securely using .NET User Secrets
- **No Hardcoded Secrets**: No API keys are stored in source code or configuration files
- **Development Only**: User secrets are only loaded in development environment

## Technology Stack

- **.NET 8**
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **SQL Server LocalDB**
- **Bootstrap 5**
- **Grok API** (for infinite tests)

## Project Structure

```
EnglishLearningApp/
├── EnglishLearningApp.Domain/          # Domain entities and interfaces
├── EnglishLearningApp.Application/      # Business logic and DTOs
├── EnglishLearningApp.Infrastructure/  # Data access and external services
└── EnglishLearningApp.Presentation/    # Web application (MVC)
```
