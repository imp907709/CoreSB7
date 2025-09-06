# CoreSB - NET Core architecture template project
     A dockerized .NET solution with clean architecture  multi-database support 

    - Multi-Database  - SQL , Mongo, Elastic
    - 3 layered - API, Domain, Infrastructure | (API, BL, DAL)
    - dockerized
    - registrations and injections split from server wrapper


## Documentation
- [Project Description](description.md) - Detailed overview and architecture
- [Detailed dev build and run commands](buildandrun.md) - EF and Docker commands

# SOLID | Vertical slice | DDD | traditional ( layered/onion/clean)

┌─────────────────────────────────────────┐
│              API Layer                  │ ← (Http, gRPC,WebSockets)
├─────────────────────────────────────────┤
│  ┌─────────────┐  ┌─────────────┐       │
│  │   Logging   │  │  Weather    │       │  ← Vertical Slices
│  │   Feature   │  │  Feature    │       │     (Business Domain - DDD goes here)
│  └─────────────┘  └─────────────┘       │
├─────────────────────────────────────────┤
│        Generic Infrastructure           │  ← Traditional Layer
│     (EF, MongoDB, Elasticsearch)        │     (Cross-cutting)
└─────────────────────────────────────────┘
