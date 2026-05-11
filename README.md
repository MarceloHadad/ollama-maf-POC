# Ollama MAF POC

A minimal console chat app connecting to a local [Ollama](https://ollama.com) instance using the [Microsoft Agents Framework (MAF)](https://github.com/microsoft/agents).

## Stack

- .NET 8
- [`Microsoft.Extensions.AI.Ollama`](https://www.nuget.org/packages/Microsoft.Extensions.AI.Ollama) — Ollama chat client
- [`Microsoft.Agents.AI`](https://www.nuget.org/packages/Microsoft.Agents.AI) — MAF agent abstraction with built-in conversation history

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Ollama](https://ollama.com) running locally
- The model you want to use pulled locally, e.g.:

  ```bash
  ollama pull llama3.2:3b
  ```

## Setup

1. Clone the repo
2. Edit `appsettings.json`:

   ```json
   {
     "OllamaUrl": "http://localhost:11434",
     "OllamaModel": "llama3.2:3b"
   }
   ```

## Running

```bash
dotnet run
```

Type your message and press Enter. Type `exit` to quit.

## Configuration

Settings are read from `appsettings.json` (gitignored). Environment variables override the file, which makes it easy to use in CI or containers:

| Key | Description |
| --- | --- |
| `OllamaUrl` | Base URL of the Ollama instance |
| `OllamaModel` | Model ID to use (must be pulled locally) |

The app exits with an error if either value is missing.
