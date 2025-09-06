# CoreSB - NET Core architecture template project
    A .NET solution demonstrating clean architecture with multi-database support.

 - Multi-Database  - SQL , Mongo, Elastic
 - 3 layered - API, Domain, Infrastructure | (API, BL, DAL)
 - dockerized
 - registrations and injections split from server wrapper

## Documentation
- [Project Description](description.md) - Detailed overview and architecture
- [Build and Run Commands](buildandrun.md) - EF and Docker commands

## Quick Start
```bash
# Run with Docker
docker-compose up -d

# Run locally
dotnet run --project CoreSBServer
```

## API Endpoints
- `GET /test` - Health check
- `POST /AddToAll` - Logging endpoint (persists to all databases)
- `GET /WeatherForecast` - Sample data endpoint
