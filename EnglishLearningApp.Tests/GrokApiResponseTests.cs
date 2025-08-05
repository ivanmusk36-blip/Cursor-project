using System.Text.Json;
using EnglishLearningApp.Application.DTOs;
using Xunit;

namespace EnglishLearningApp.Tests;

public class GrokApiResponseTests
{
    [Fact]
    public void ParseStandardGrokResponse_ShouldWork()
    {
        // Standard Grok API response format
        var standardResponse = @"{
            ""id"": ""chatcmpl-123"",
            ""object"": ""chat.completion"",
            ""created"": 1677652288,
            ""model"": ""grok-4-latest"",
            ""choices"": [
                {
                    ""index"": 0,
                    ""message"": {
                        ""role"": ""assistant"",
                        ""content"": ""{\""rule\"": \""articles (a, an, the)\"", \""questions\"": [{\""question\"": \""Which article is correct: I saw _____ elephant at the zoo.\"", \""correctAnswer\"": \""an\"", \""options\"": [\""a\"", \""an\"", \""the\"", \""no article\""], \""explanation\"": \""Use 'an' before words that begin with a vowel sound.\""}]}""
                    },
                    ""finish_reason"": ""stop""
                }
            ],
            ""usage"": {
                ""prompt_tokens"": 9,
                ""completion_tokens"": 12,
                ""total_tokens"": 21
            }
        }";

        var responseData = JsonSerializer.Deserialize<JsonElement>(standardResponse);
        
        var choices = responseData.GetProperty("choices");
        var firstChoice = choices[0];
        var message = firstChoice.GetProperty("message");
        var contentText = message.GetProperty("content").GetString() ?? "";

        // Extract JSON from the content
        var jsonStart = contentText.IndexOf('{');
        var jsonEnd = contentText.LastIndexOf('}') + 1;
        
        Assert.True(jsonStart >= 0, "Should find JSON start");
        Assert.True(jsonEnd > jsonStart, "Should find JSON end");
        
        var jsonContent = contentText.Substring(jsonStart, jsonEnd - jsonStart);
        var grokResponse = JsonSerializer.Deserialize<GrokTestResponse>(jsonContent);
        
        Assert.NotNull(grokResponse);
        Assert.Equal("articles (a, an, the)", grokResponse.Rule);
        Assert.Single(grokResponse.Questions);
        Assert.Equal("Which article is correct: I saw _____ elephant at the zoo.", grokResponse.Questions[0].Question);
        Assert.Equal("an", grokResponse.Questions[0].CorrectAnswer);
        Assert.Equal(4, grokResponse.Questions[0].Options.Count);
    }

    [Fact]
    public void ParseGrokResponseWithMultipleQuestions_ShouldWork()
    {
        var multiQuestionResponse = @"{
            ""choices"": [
                {
                    ""message"": {
                        ""content"": ""{\""rule\"": \""verb tenses\"", \""questions\"": [{\""question\"": \""Choose the correct form: She _____ to the store yesterday.\"", \""correctAnswer\"": \""went\"", \""options\"": [\""go\"", \""goes\"", \""went\"", \""going\""], \""explanation\"": \""Use past tense for completed actions.\""}, {\""question\"": \""What's the past tense of 'run'?\"", \""correctAnswer\"": \""ran\"", \""options\"": [\""runned\"", \""ran\"", \""running\"", \""runs\""], \""explanation\"": \""'Ran' is the irregular past tense of 'run'.\""}]}""
                    }
                }
            ]
        }";

        var responseData = JsonSerializer.Deserialize<JsonElement>(multiQuestionResponse);
        var choices = responseData.GetProperty("choices");
        var firstChoice = choices[0];
        var message = firstChoice.GetProperty("message");
        var contentText = message.GetProperty("content").GetString() ?? "";

        var jsonStart = contentText.IndexOf('{');
        var jsonEnd = contentText.LastIndexOf('}') + 1;
        var jsonContent = contentText.Substring(jsonStart, jsonEnd - jsonStart);
        var grokResponse = JsonSerializer.Deserialize<GrokTestResponse>(jsonContent);
        
        Assert.NotNull(grokResponse);
        Assert.Equal("verb tenses", grokResponse.Rule);
        Assert.Equal(2, grokResponse.Questions.Count);
        Assert.Equal("went", grokResponse.Questions[0].CorrectAnswer);
        Assert.Equal("ran", grokResponse.Questions[1].CorrectAnswer);
    }

    [Fact]
    public void ParseGrokResponseWithExtraText_ShouldExtractJson()
    {
        // Sometimes the AI might add extra text before or after the JSON
        var responseWithExtraText = @"{
            ""choices"": [
                {
                    ""message"": {
                        ""content"": ""Here's your grammar test:\n\n{\""rule\"": \""pronouns\"", \""questions\"": [{\""question\"": \""Select the right pronoun: _____ is my favorite book.\"", \""correctAnswer\"": \""It\"", \""options\"": [\""Me\"", \""I\"", \""It\"", \""Mine\""], \""explanation\"": \""Use 'It' for objects.\""}]}\n\nI hope this helps with your English learning!\""""
                    }
                }
            ]
        }";

        var responseData = JsonSerializer.Deserialize<JsonElement>(responseWithExtraText);
        var choices = responseData.GetProperty("choices");
        var firstChoice = choices[0];
        var message = firstChoice.GetProperty("message");
        var contentText = message.GetProperty("content").GetString() ?? "";

        var jsonStart = contentText.IndexOf('{');
        var jsonEnd = contentText.LastIndexOf('}') + 1;
        var jsonContent = contentText.Substring(jsonStart, jsonEnd - jsonStart);
        var grokResponse = JsonSerializer.Deserialize<GrokTestResponse>(jsonContent);
        
        Assert.NotNull(grokResponse);
        Assert.Equal("pronouns", grokResponse.Rule);
        Assert.Single(grokResponse.Questions);
        Assert.Equal("It", grokResponse.Questions[0].CorrectAnswer);
    }

    [Fact]
    public void ParseGrokResponseWithEscapedQuotes_ShouldWork()
    {
        // Test with escaped quotes in the content
        var responseWithEscapedQuotes = @"{
            ""choices"": [
                {
                    ""message"": {
                        ""content"": ""{\""rule\"": \""prepositions\"", \""questions\"": [{\""question\"": \""Choose the correct preposition: I'm interested _____ learning English.\"", \""correctAnswer\"": \""in\"", \""options\"": [\""in\"", \""on\"", \""at\"", \""for\""], \""explanation\"": \""Use 'in' with 'interested'.\""}]}""
                    }
                }
            ]
        }";

        var responseData = JsonSerializer.Deserialize<JsonElement>(responseWithEscapedQuotes);
        var choices = responseData.GetProperty("choices");
        var firstChoice = choices[0];
        var message = firstChoice.GetProperty("message");
        var contentText = message.GetProperty("content").GetString() ?? "";

        var jsonStart = contentText.IndexOf('{');
        var jsonEnd = contentText.LastIndexOf('}') + 1;
        var jsonContent = contentText.Substring(jsonStart, jsonEnd - jsonStart);
        var grokResponse = JsonSerializer.Deserialize<GrokTestResponse>(jsonContent);
        
        Assert.NotNull(grokResponse);
        Assert.Equal("prepositions", grokResponse.Rule);
        Assert.Single(grokResponse.Questions);
        Assert.Equal("in", grokResponse.Questions[0].CorrectAnswer);
    }

    [Fact]
    public void ParseGrokResponseWithMalformedJson_ShouldHandleGracefully()
    {
        // Test with malformed JSON that might be returned
        var malformedResponse = @"{
            ""choices"": [
                {
                    ""message"": {
                        ""content"": ""I apologize, but I cannot generate the test in the requested format. Here's a simple grammar question instead: What is the correct article for 'elephant'? The answer is 'an'.\""""
                    }
                }
            ]
        }";

        var responseData = JsonSerializer.Deserialize<JsonElement>(malformedResponse);
        var choices = responseData.GetProperty("choices");
        var firstChoice = choices[0];
        var message = firstChoice.GetProperty("message");
        var contentText = message.GetProperty("content").GetString() ?? "";

        var jsonStart = contentText.IndexOf('{');
        var jsonEnd = contentText.LastIndexOf('}') + 1;
        
        // Should not find valid JSON
        Assert.True(jsonStart < 0 || jsonEnd <= jsonStart);
    }

    [Fact]
    public void ParseGrokResponseWithNoJson_ShouldHandleGracefully()
    {
        // Test with response that has no JSON
        var noJsonResponse = @"{
            ""choices"": [
                {
                    ""message"": {
                        ""content"": ""I'm sorry, I cannot generate grammar tests at the moment. Please try again later.\""""
                    }
                }
            ]
        }";

        var responseData = JsonSerializer.Deserialize<JsonElement>(noJsonResponse);
        var choices = responseData.GetProperty("choices");
        var firstChoice = choices[0];
        var message = firstChoice.GetProperty("message");
        var contentText = message.GetProperty("content").GetString() ?? "";

        var jsonStart = contentText.IndexOf('{');
        var jsonEnd = contentText.LastIndexOf('}') + 1;
        
        // Should not find valid JSON
        Assert.True(jsonStart < 0 || jsonEnd <= jsonStart);
    }

    [Fact]
    public void ParseGrokResponseWithNestedJson_ShouldExtractOuterJson()
    {
        // Test with nested JSON structures
        var nestedJsonResponse = @"{
            ""choices"": [
                {
                    ""message"": {
                        ""content"": ""{\""rule\"": \""modal verbs\"", \""questions\"": [{\""question\"": \""Choose the correct modal: You _____ study harder.\"", \""correctAnswer\"": \""should\"", \""options\"": [\""can\"", \""should\"", \""must\"", \""will\""], \""explanation\"": \""Use 'should' for advice.\""}]}""
                    }
                }
            ]
        }";

        var responseData = JsonSerializer.Deserialize<JsonElement>(nestedJsonResponse);
        var choices = responseData.GetProperty("choices");
        var firstChoice = choices[0];
        var message = firstChoice.GetProperty("message");
        var contentText = message.GetProperty("content").GetString() ?? "";

        var jsonStart = contentText.IndexOf('{');
        var jsonEnd = contentText.LastIndexOf('}') + 1;
        var jsonContent = contentText.Substring(jsonStart, jsonEnd - jsonStart);
        var grokResponse = JsonSerializer.Deserialize<GrokTestResponse>(jsonContent);
        
        Assert.NotNull(grokResponse);
        Assert.Equal("modal verbs", grokResponse.Rule);
        Assert.Single(grokResponse.Questions);
        Assert.Equal("should", grokResponse.Questions[0].CorrectAnswer);
    }

    [Fact]
    public void TestJsonExtraction_ShouldWork()
    {
        // Test the JSON extraction logic with a simple case
        var contentText = "Here's your test: {\"rule\": \"test\", \"questions\": [{\"question\": \"test?\", \"correctAnswer\": \"a\", \"options\": [\"a\", \"b\", \"c\", \"d\"], \"explanation\": \"test\"}]} End of test";
        
        var jsonStart = contentText.IndexOf('{');
        var jsonEnd = contentText.LastIndexOf('}') + 1;
        
        Assert.True(jsonStart >= 0, "Should find JSON start");
        Assert.True(jsonEnd > jsonStart, "Should find JSON end");
        
        var jsonContent = contentText.Substring(jsonStart, jsonEnd - jsonStart);
        var grokResponse = JsonSerializer.Deserialize<GrokTestResponse>(jsonContent);
        
        Assert.NotNull(grokResponse);
        Assert.Equal("test", grokResponse.Rule);
        Assert.Single(grokResponse.Questions);
        Assert.Equal("test?", grokResponse.Questions[0].Question);
        Assert.Equal("a", grokResponse.Questions[0].CorrectAnswer);
    }
} 