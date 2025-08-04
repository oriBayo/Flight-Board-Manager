# Flight Board Manager

A real-time flight management system built with ASP.NET Core and React. Features live updates via SignalR, flight status calculation, and a clean modern interface.

<img width="1370" height="761" alt="Screenshot 2025-08-04 at 8 18 20" src="https://github.com/user-attachments/assets/a718ea73-ecff-4c7c-b937-f4d8747cdf67" />
<img width="1533" height="687" alt="Screenshot 2025-08-04 at 8 18 30" src="https://github.com/user-attachments/assets/f6da49a1-2431-4134-843e-6a1964c66c93" />


## Quick Start

### Prerequisites

- .NET 8.0 SDK
- Node.js 18+
- Docker (optional)

### Run with Docker

```bash
git clone <repo-url>
cd FlightBoardManager
docker-compose up --build
```

Access at: http://localhost:3000

### Manual Setup

**Backend:**

```bash
cd backend/FlightBoard.API
dotnet run
```

**Frontend:**

```bash
cd frontend
npm install
npm start
```

## Features

- **Real-time updates** - Live flight status changes via SignalR
- **Flight management** - Add, delete, and filter flights
- **Status calculation** - Server-side flight status logic
- **Search & filter** - Filter by status and destination
- **Clean architecture** - Separated layers with dependency injection

## Tech Stack

**Backend:**

- **ASP.NET Core 8.0** Web API
- **Entity Framework Core** (v9.0.7) with SQLite provider
- **SignalR** (v1.2.0) for real-time communication
- **FluentValidation** (v12.0.0) for input validation
- **AutoMapper** (v12.0.1) for object mapping
- **Moq** (v4.20.72) & **FluentAssertions** (v8.5.0) for testing

**Frontend:**

- **React 18** with TypeScript
- **Redux Toolkit** (v2.8.2) & **React Redux** (v9.2.0) for state management
- **TanStack Query** (v5.83.0) for server state management
- **Axios** (v1.11.0) for HTTP requests
- **Tailwind CSS** (v3.4.17) for styling
- **Motion** (v12.23.11) for animations
- **@microsoft/signalr** (v8.0.7) for real-time updates
- **Day.js** (v1.11.13) for date handling
- **Lucide React** (v0.532.0) for icons
- **React Hot Toast** (v2.5.2) for notifications
- **React Spinners** (v0.17.0) for loading indicators

## Project Structure

```
FlightBoardManager/
├── FlightBoardManager.sln          
├── backend/
│   ├── FlightBoard.API/           
│   │   ├── Controllers/           
│   │   ├── Services/             
│   │   ├── Hubs/                 
│   │   ├── Properties/           
│   │   ├── Program.cs            
│   │   ├── appsettings.json      
│   │   ├── FlightBoard.API.csproj
│   │   ├── flightboard.db        
│   │   └── obj/, bin/            
│   │
│   ├── FlightBoard.Domain/        
│   │   ├── Entities/             
│   │   ├── Enums/               
│   │   ├── Exceptions/          
│   │   ├── Interfaces/          
│   │   └── FlightBoard.Domain.csproj
│   │
│   ├── FlightBoard.Application/   
│   │   ├── Services/            
│   │   ├── Interfaces/          
│   │   ├── DTOs/               
│   │   ├── Validators/         
│   │   ├── Mapping/            
│   │   ├── DependencyInjection/ 
│   │   └── FlightBoard.Application.csproj
│   │
│   ├── FlightBoard.Infrastructure/ 
│   └── FlightBoard.Tests/         
│
└── frontend/                      
    ├── src/
    │   ├── components/           
    │   ├── hooks/               
    │   ├── store/               
    │   ├── actions/             
    │   ├── types/               
    │   ├── utils/               
    │   ├── constants/           
    │   ├── App.tsx              
    │   ├── index.tsx            
    │   └── index.css           
    │
    ├── public/                   
    ├── package.json             
    ├── tsconfig.json            
    ├── tailwind.config.js       
    └── postcss.config.js        
```

## Architecture

Built with **Clean Architecture** principles:

- **Domain Layer** - Core business logic and entities
- **Application Layer** - Use cases and service interfaces
- **Infrastructure Layer** - Data persistence and external integrations
- **API Layer** - Controllers and SignalR hubs

Frontend uses modern React patterns with server state managed by TanStack Query and client state by Redux Toolkit.

## API Endpoints

- `GET /api/flights` - Get all flights with status
- `POST /api/flights` - Create new flight
- `DELETE /api/flights/{id}` - Delete flight
- `GET /api/flights/search?status={status}&destination={destination}` - Search flights

**SignalR Hub:** `/flighthub` for real-time updates

## Flight Status Logic

Status calculated based on current time vs departure time:

- **Scheduled** - More than 30 minutes before departure
- **Boarding** - 30 minutes before to departure time
- **Departed** - Departure time to 60 minutes after
- **Landed** - More than 60 minutes after departure

## Testing

Run backend tests:

```bash
cd backend
dotnet test
```

Run frontend tests:

```bash
cd frontend
npm test
```

## Configuration

**Backend** (`backend/FlightBoard.API/appsettings.json`):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=flightboard.db"
  },
  "CORS": {
    "AllowedOrigins": ["http://localhost:3000"]
  }
}
```

**Frontend** (environment variables):

```env
REACT_APP_API_URL=http://localhost:5000/api
REACT_APP_SIGNALR_URL=http://localhost:5000/flighthub
```

## Docker Setup

The project includes Docker configuration for easy deployment:

**File Structure:**

```
FlightBoardManager/
├── docker-compose.yml          # Root level
├── backend/
│   ├── Dockerfile              # Backend container
│   └── .dockerignore
└── frontend/
    ├── Dockerfile              # Frontend container
    ├── nginx.conf              # Nginx configuration
    └── .dockerignore
```

**Commands:**

```bash
# Build and run containers
docker-compose up --build

# Run in background
docker-compose up --build -d

# Stop containers
docker-compose down

# View logs
docker-compose logs -f
```

**Access Points:**

- Frontend: http://localhost:3000
- Backend API: http://localhost:5000/api
- SignalR Hub: http://localhost:5000/flighthub
