# Bob’s Corn 🌽

A simple full-stack application that allows clients to buy corn with a rate limit of **1 corn per client per minute**.

---

## 🏗️ Architecture

The backend follows a **Clean Architecture** approach with clear separation of concerns:

- **Domain**: Core business entities (e.g., `CornPurchase`)
- **Application**: Use cases, CQRS (MediatR), validation (FluentValidation)
- **Infrastructure**: Data access (Dapper), rate limiter, database connection
- **API**: Controllers, configuration, exception handling

Key design decisions:

- Business logic is isolated in the application layer
- Validation is handled via a MediatR pipeline (`ValidationBehavior`)
- Errors are centralized using a global exception handler
- Infrastructure details (Dapper, SQL) are abstracted behind interfaces

---

## ⚙️ Technologies

### Backend
- .NET 9
- ASP.NET Core Web API
- MediatR (CQRS pattern)
- FluentValidation
- Dapper
- SQL Server
- Swagger (Swashbuckle)

### Frontend
- React + Vite
- TypeScript
- React Router
- React Hook Form
- Axios
- Bootstrap

---

## 🔐 Client Identification

Since authentication is out of scope, the frontend generates a `clientId` using `crypto.randomUUID()` and stores it in `localStorage`.

This ID is sent via the `X-Client-Id` header to simulate a stable client identity.

---

## 📦 Configuration

### Backend

The project includes an `appsettings.example.json` file:

```json
{
  "ConnectionStrings": {
    "BobsCorn": "Server=localhost;Database=BobsCornDb;Trusted_Connection=True;"
  },
  "Cors": {
    "AllowedOrigins": [
      "http://localhost:5173"
    ]
  }
}
```

Create your own `appsettings.json` based on this file and update values as needed.

---

### Frontend

Create a `.env` file based on `.env.example`:

```env
VITE_API_URL=https://localhost:7000
```

---

## 🚀 Running the project

### Backend

```bash
cd bobs-corn-api
dotnet restore
dotnet run
```

### Frontend

```bash
cd bobs-corn-frontend
npm install
npm run dev
```

---

## 📌 Notes

- Rate limiting is implemented in-memory for simplicity
- Database scripts are included to create schema
- The system assumes unlimited inventory (focus is on rate limiting)

---

## 🔮 Future Improvements

- Replace client-generated ID with authentication
- Use distributed rate limiting (e.g., Redis)
- Add integration tests for persistence
- Improve UI/UX and add loading states
