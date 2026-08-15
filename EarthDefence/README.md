# 🌍 EarthDefence

**EarthDefence** is an asynchronous idle/strategy game built with .NET 10 and Azure Serverless Infrastructure. Players manage defense operations, complete timed orbital missions, and collect resource yields stored in real-time in Azure Cosmos DB.

---

## 🛠️ Architecture & Tech Stack

* **Backend API:** ASP.NET Core Minimal API (.NET 10)
* **Database:** Azure Cosmos DB for NoSQL (Lifetime Free Tier, `/playerId` Partition Key)
* **Frontend (Planned):** Blazor WebAssembly / Razor Components (Responsive Mobile-First UI)
* **Hosting:** Azure App Service (API) & Azure Static Web Apps (Blazor)

---

## 🚀 Current Progress

- [x] **Core Domain Models:** Defined `PlayerState`, `PlayerTask`, `Currencies`, and `ActiveTaskState`.
- [x] **Repository Layer:** Implemented `CosmosPlayerRepo` using Azure Cosmos DB NoSQL SDK.
- [x] **Database Setup:** Provisioned `EarthDefenceDb` database and `PlayerStates` container on Azure.
- [x] **Game API Endpoints:** Built Minimal API endpoints for fetching player state, starting timed tasks, and claiming rewards.
- [x] **Live Azure Deployment:** Deployed API to Azure App Service and connected live environment variables.

---

## 🗺️ Next Steps & Roadmap

1. **Shared Domain Library:** Extract domain models into a shared `EarthDefence.Shared` C# class library.
2. **Blazor WebApp Setup:** Scaffold Blazor WebAssembly UI using Tailwind CSS / MudBlazor.
3. **Typed API Service:** Implement `GameApiService.cs` in Blazor to consume the live Azure API.
4. **Real-time Task Timer Component:** Build `TaskOperations.razor` with a 1-second C# `PeriodicTimer` for countdowns and rewards.
5. **Session Management:** Persist active `playerId` in browser `localStorage`.
6. **Azure CI/CD:** Deploy Blazor WebAssembly frontend to Azure Static Web Apps via GitHub Actions.

---

## ⚙️ Running Locally

1. Clone the repository:
   ```bash
   git clone [https://github.com/YOUR_USERNAME/EarthDefence.git](https://github.com/YOUR_USERNAME/EarthDefence.git)
   cd EarthDefence

2. Configure local secret:
   dotnet user-secrets set "CosmosDb:ConnectionString" "<YOUR_COSMOS_CONNECTION_STRING>"

3. Run the API:
    dotnet run --project EarthDefence