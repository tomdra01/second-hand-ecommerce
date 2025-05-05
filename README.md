# Second-Hand E-Commerce Backend

## Tech Stack
- MongoDB (NoSQL)
- Redis (Caching)
- MinIO (Cloud Storage)
- .NET 8 Web API
- CQRS Pattern
- MongoDB Transactions

## Clean Architecture
- `Domain`: Entities and core models
- `Application`: DTOs, CQRS logic, interfaces
- `Infrastructure`: External dependencies (Mongo, Redis, MinIO)
- `WebAPI`: Entry point, controllers, DI

## Getting Started

1. Clone the repo
2. Run Docker:
   ```bash
   docker-compose up -d
