using System.Text.Json;
using EnglishLearningApp.Application.DTOs;
using Xunit;

namespace EnglishLearningApp.Tests;

public class DebugJsonTests
{
    [Fact]
    public void DebugSimpleJsonDeserialization()
    {
        // Test with a simple JSON string
        var simpleJson = "{\"rule\": \"test rule\", \"questions\": [{\"question\": \"test question?\", \"correctAnswer\": \"a\", \"options\": [\"a\", \"b\", \"c\", \"d\"], \"explanation\": \"test explanation\"}]}";
        
        Console.WriteLine($"Input JSON: {simpleJson}");
        
        var grokResponse = JsonSerializer.Deserialize<GrokTestResponse>(simpleJson);
        
        Console.WriteLine($"Deserialized Rule: '{grokResponse?.Rule}'");
        Console.WriteLine($"Questions Count: {grokResponse?.Questions?.Count ?? 0}");
        
        if (grokResponse?.Questions?.Count > 0)
        {
            Console.WriteLine($"First Question: '{grokResponse.Questions[0].Question}'");
            Console.WriteLine($"Correct Answer: '{grokResponse.Questions[0].CorrectAnswer}'");
        }
        
        Assert.NotNull(grokResponse);
        Assert.Equal("test rule", grokResponse.Rule);
        Assert.Single(grokResponse.Questions);
    }

    [Fact]
    public void DebugJsonExtraction()
    {
        // Test the JSON extraction from content
        var contentText = "Here's your test: {\"rule\": \"test rule\", \"questions\": [{\"question\": \"test question?\", \"correctAnswer\": \"a\", \"options\": [\"a\", \"b\", \"c\", \"d\"], \"explanation\": \"test explanation\"}]} End of test";
        
        Console.WriteLine($"Content Text: {contentText}");
        
        var jsonStart = contentText.IndexOf('{');
        var jsonEnd = contentText.LastIndexOf('}') + 1;
        
        Console.WriteLine($"JSON Start: {jsonStart}, JSON End: {jsonEnd}");
        
        var jsonContent = contentText.Substring(jsonStart, jsonEnd - jsonStart);
        Console.WriteLine($"Extracted JSON: {jsonContent}");
        
        var grokResponse = JsonSerializer.Deserialize<GrokTestResponse>(jsonContent);
        
        Console.WriteLine($"Deserialized Rule: '{grokResponse?.Rule}'");
        Console.WriteLine($"Questions Count: {grokResponse?.Questions?.Count ?? 0}");
        
        Assert.NotNull(grokResponse);
        Assert.Equal("test rule", grokResponse.Rule);
        Assert.Single(grokResponse.Questions);
    }

    [Fact]
    public void DebugGrokApiResponse()
    {
        // Test with the actual Grok API response format
        var grokResponse = @"{
            ""choices"": [
                {
                    ""message"": {
                        ""content"": ""{\""rule\"": \""test rule\"", \""questions\"": [{\""question\"": \""test question?\"", \""correctAnswer\"": \""a\"", \""options\"": [\""a\"", \""b\"", \""c\"", \""d\""], \""explanation\"": \""test explanation\""}]}""
                    }
                }
            ]
        }";

        var responseData = JsonSerializer.Deserialize<JsonElement>(grokResponse);
        var choices = responseData.GetProperty("choices");
        var firstChoice = choices[0];
        var message = firstChoice.GetProperty("message");
        var contentText = message.GetProperty("content").GetString() ?? "";

        Console.WriteLine($"Content from API: {contentText}");

        var jsonStart = contentText.IndexOf('{');
        var jsonEnd = contentText.LastIndexOf('}') + 1;
        
        Console.WriteLine($"JSON Start: {jsonStart}, JSON End: {jsonEnd}");
        
        var jsonContent = contentText.Substring(jsonStart, jsonEnd - jsonStart);
        Console.WriteLine($"Extracted JSON: {jsonContent}");
        
        var grokTestResponse = JsonSerializer.Deserialize<GrokTestResponse>(jsonContent);
        
        Console.WriteLine($"Deserialized Rule: '{grokTestResponse?.Rule}'");
        Console.WriteLine($"Questions Count: {grokTestResponse?.Questions?.Count ?? 0}");
        
        Assert.NotNull(grokTestResponse);
        Assert.Equal("test rule", grokTestResponse.Rule);
        Assert.Single(grokTestResponse.Questions);
    }
} 