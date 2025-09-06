# CoreSB_7 - Multi-Database NET 7 architecture template project

A .NET 7.0 solution demonstrating clean architecture with multi-database support for logging and data persistence. Features Entity Framework (SQL Server/PostgreSQL), MongoDB, and Elasticsearch integration with generic repository patterns.

## Architecture - 3 Layered
- **CoreSBServer**: ASP.NET Core Web API entry point
- **CoreSBBL**: Business logic layer with logging services
- **CoreSBShared**: Shared utilities and infrastructure abstractions

## Key Features
- **Multi-Database Support**: SQL Server, MongoDB, and Elasticsearch
- **Generic Repository Pattern**: Multiple implementations for different ID types
- **Cross-Database Logging**: Persists logs to all three databases simultaneously
- **Docker Environment**: Complete containerized setup with all services
- **Clean Architecture**: Clear separation of concerns across layers

## Technology Stack
- .NET 7.0 with C# 11 features
- Entity Framework Core 7.0.5
- MongoDB Driver 2.19.2
- Elasticsearch 8.5.3 with NEST client
- Docker Compose for development

## API Endpoints
- `GET /test` - Health check
- `POST /AddToAll` - Logging endpoint (persists to all databases)
- `GET /WeatherForecast` - Sample data endpoint
