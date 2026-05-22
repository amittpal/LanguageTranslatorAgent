

using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;

IChatClient chatClient = new ChatClient("gpt-4o-mini",
    new ApiKeyCredential("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")
    ,new OpenAIClientOptions { Endpoint=new Uri("https://models.github.ai/inference") }).AsIChatClient();

AIAgent frenchAgent = new ChatClientAgent(chatClient,
    new ChatClientAgentOptions { 
        Name= "FrenchAgent"
    });

AIAgent spanishAgent = new ChatClientAgent(chatClient,
    new ChatClientAgentOptions
    {
        Name = "SpanishAgent"
    });

string qualityReviewDesc="""
    you are a multilingual translation quality reviewer. Check translation for grammer accuracy, tone consistency, and cultural fit compared to the original English text.
    
    Give a brief summary with quality rating (Excellent / Good / needs Review).
    
    Example output:
    Quality: Excellent 
    Feedback: Accurate translation, friendly tone preserved, minor punctuation tweaks only.
    """;
AIAgent qualityRevieweAgent = new ChatClientAgent(chatClient,
    new ChatClientAgentOptions
    {
        Name = "qualityRevieweAgent"
    });

AIAgent workflowAgent=AgentWorkflowBuilder.BuildSequential(frenchAgent,spanishAgent,qualityRevieweAgent).AsAIAgent();

bool running = true;
while (running)
{
    Console.Write("\nYou (or 'exit' to quit): ");
    string userInput = Console.ReadLine() ?? string.Empty;
    
    if (userInput.ToLower() == "exit")
    {
        running = false;
        break;
    }
    
    if (string.IsNullOrWhiteSpace(userInput))
    {
        continue;
    }
    
    string instructedInput = $"""
        You are a translation agent. Follow these steps in order:
        1. FrenchAgent: Translate this text ONLY into French. Return ONLY the French translation, nothing else.
        2. SpanishAgent: Translate the French text ONLY into Spanish. Return ONLY the Spanish translation, nothing else.
        3. qualityRevieweAgent: Review the Spanish translation for quality, accuracy, and tone.
        
        Original English text to translate: {userInput}
        """;
    
    AgentResponse res = await workflowAgent.RunAsync(instructedInput);
    Console.WriteLine();

    foreach(var r in res.Messages)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(r.AuthorName);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(r.Text);

        Console.WriteLine();
    }
}