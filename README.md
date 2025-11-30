# IdeaBox

A small but realistic **idea sharing backend**, built to practice modern .NET, clean layering, authentication, voting and pagination.

## 🔧 Tech Stack

- .NET 10 Web API (`IdeaBox.Api`)
- Layered architecture:
  - `IdeaBox.Domain`
  - `IdeaBox.Application`
  - `IdeaBox.Infrastructure`
  - `IdeaBox.Api`
- Entity Framework Core + **SQLite**
- JWT Authentication
- Serilog logging
- FluentValidation
- Voting (like) system + pagination

## 🚀 Getting Started

```bash
git clone https://github.com/<your-username>/IdeaBox.git
cd IdeaBox

dotnet restore
dotnet build

# apply EF Core migrations to SQLite
dotnet ef database update -p src/IdeaBox.Infrastructure/IdeaBox.Infrastructure.csproj -s src/IdeaBox.Api/IdeaBox.Api.csproj

# run the API
cd src/IdeaBox.Api
dotnet run
