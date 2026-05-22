# Language Translator Multi-Agent

A multi-agent translation system built with **Microsoft.Agents.AI** that translates English text to French and Spanish, then performs quality review.

## Features

- **FrenchAgent**: Translates text from English to French
- **SpanishAgent**: Translates the French translation to Spanish  
- **QualityReviewAgent**: Reviews translations for grammar, accuracy, tone consistency, and cultural fit
- **Sequential Workflow**: Agents work in a pipeline for coordinated translation

## How It Works

1. Enter English text at the prompt
2. **FrenchAgent** translates to French
3. **SpanishAgent** translates the French version to Spanish
4. **QualityReviewAgent** evaluates the final translation with a quality rating (Excellent/Good/Needs Review)
5. All agent responses are displayed with color-coded output

## Usage

```bash
dotnet run
```

Enter text to translate:
```
You (or 'exit' to quit): Hello, how are you?
```

Type `exit` to quit the application.

## Requirements

- .NET 10.0 or higher
- GitHub Copilot AI access via GitHub Models API
- Valid GitHub PAT token (already configured in Program.cs)

## Dependencies

- Microsoft.Agents.AI
- Microsoft.Agents.AI.Workflows
- Microsoft.Extensions.AI
- OpenAI (for GPT-4o-mini)

## Technologies

- **Framework**: .NET 10.0
- **AI Model**: GPT-4o-mini via GitHub Models
- **Agent Framework**: Microsoft Agents.AI
- **Language**: C#
