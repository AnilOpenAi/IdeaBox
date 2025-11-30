# IdeaBox

Küçük ama gerçekçi bir **fikir paylaşım backend’i**.  
Tek amaç: modern .NET ile temiz bir API tasarlayıp, onboarding / auth / voting / pagination gibi temel konuları pratik etmek.

## 🔧 Tech Stack

- .NET 10 Web API (`IdeaBox.Api`)
- Katmanlı yapı:
  - `IdeaBox.Domain`
  - `IdeaBox.Application`
  - `IdeaBox.Infrastructure`
  - `IdeaBox.Api`
- Entity Framework Core + **SQLite**
- JWT Authentication
- Serilog logging
- FluentValidation
- Like (vote) sistemi + pagination

## 🚀 Çalıştırma

```bash
git clone https://github.com/<kullanıcı-adın>/IdeaBox.git
cd IdeaBox

dotnet restore
dotnet build

# database (SQLite) migration
dotnet ef database update -p src/IdeaBox.Infrastructure/IdeaBox.Infrastructure.csproj -s src/IdeaBox.Api/IdeaBox.Api.csproj

# api'yi çalıştır
cd src/IdeaBox.Api
dotnet run
