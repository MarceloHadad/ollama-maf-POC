using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

var ollamaUrl = config["OllamaUrl"];
var modelId = config["OllamaModel"];

if (string.IsNullOrWhiteSpace(ollamaUrl) || string.IsNullOrWhiteSpace(modelId))
{
    await Console.Error.WriteLineAsync("Error: OllamaUrl and OllamaModel must be set in appsettings.json or environment variables.");
    return;
}

var chatClient = new OllamaChatClient(new Uri(ollamaUrl), modelId: modelId);

AIAgent agent = chatClient.AsAIAgent(instructions: "You are a helpful assistant. Always respond in the same language the user writes in. Never switch languages unless the user does first.");

var session = await agent.CreateSessionAsync();

Console.WriteLine($"Chat with Ollama ({modelId}). Type 'exit' to quit.\n");

while (true)
{
    Console.Write("You: ");
    var input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input)) continue;
    if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
        break;

    var response = await agent.RunAsync(input, session, null, CancellationToken.None);
    Console.WriteLine($"\nAgent: {response.Text}\n");
}

Console.WriteLine("Bye.");
