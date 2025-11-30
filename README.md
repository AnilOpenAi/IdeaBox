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

# apply EF Core migra

The API will listen on something like:

http://localhost:5145

Swagger UI:

http://localhost:5145/swagger

(Adjust the port according to what Kestrel prints in the console.)

🔐 Authentication Flow
1. Register

POST /api/auth/register

Request body:

{
  "email": "test@example.com",
  "password": "Test123!"
}

Example response:

{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}

2. Login

POST /api/auth/login

Same request body format, response also returns a JWT token.

3. Use the token

For protected endpoints, send the token in the Authorization header:

Authorization: Bearer <token>

Works with any HTTP client (Postman, Thunder Client, curl, etc.).

💡 Ideas Endpoints
Create an idea

POST /api/ideas

Headers:

Authorization: Bearer <token>
Content-Type: application/json

Body:

{
  "title": "My first idea",
  "description": "This is a test idea for IdeaBox."
}

Returns 201 Created with the new idea id.

List ideas (with pagination)

GET /api/ideas?page=1&pageSize=10

Example response:

{
  "items": [
    {
      "id": "GUID",
      "title": "My first idea",
      "description": "This is a test idea for IdeaBox.",
      "status": 0,
      "createdAt": "2025-12-01T12:34:56Z",
      "voteCount": 1
    }
  ],
  "page": 1,
  "pageSize": 10,
  "totalCount": 1,
  "totalPages": 1
}

Like / Unlike an idea

Like

POST /api/ideas/{id}/like

Unlike

POST /api/ideas/{id}/unlike

Headers for both:

Authorization: Bearer <token>

No request body required.
On success they return 204 No Content.
The voteCount field in the idea list reflects the current like count.

🗄️ Database

Development uses a local SQLite database file: ideabox.db.

If the schema changes during development and you don’t care about existing data, the easiest reset is:

Delete ideabox.db

Run dotnet ef database update again

This recreates the database from the latest migrations.

