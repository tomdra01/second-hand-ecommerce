# Second–Hand E-Commerce Backend

This is a backend service for a second-hand marketplace platform, built with .NET 8 and MongoDB.

## 🛠 Tech Stack

- **.NET 8 Web API** – Backend application
- **MongoDB** – NoSQL database for item listings
- **Redis** *(Planned)* – In-memory caching layer
- **MinIO** *(Planned)* – Cloud-native object storage for image uploads
- **CQRS Pattern** – Separation of read/write responsibilities
- **MongoDB Transactions** – Atomic operations (if needed)

## 🧱 Clean Architecture Overview

- **Domain**: Contains core business entities like `ItemListing.cs`.
- **Application**: Contains use-case logic, service interfaces, DTOs, and mappers.
- **Infrastructure**: External integrations like MongoDB, Redis (planned), and MinIO (planned).
- **API**: Entry point with controllers, config classes, dependency injection, and Swagger.

## 🚀 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/tomdra01/second-hand-ecommerce.git
cd second-hand-ecommerce
```

### 2. Run Required Services with Docker

```bash
docker-compose up -d
```

This spins up:

MongoDB on port 27017

Redis (planned) on port 6379

MinIO (planned) on port 9000

### 3. Run the .NET Backend
From the src/API/ folder:

```bash
dotnet run
```

API will be available at:
http://localhost:5040

### 4. Test Endpoints

Use Postman or similar tool to hit:

POST /api/itemlistings – Create a new item listing (JSON payload)

GET /api/itemlistings – Get all listings

GET /api/itemlistings/{id} – Get listing by ID

### 🗂 Project Structure

```pgsql
second-hand-ecommerce/
├── API/
│   ├── Config/
│   ├── Controllers/
│   ├── Program.cs
│   └── appsettings.json
├── Application/
│   ├── DTOs/
│   ├── Interfaces/
│   ├── Mappers/
│   └── Services/
├── Domain/
│   └── Entities/
├── Infrastructure/
│   ├── Common/
│   ├── Data/
│   ├── ItemListings/
│   └── Storage/
└── docker-compose.yml
```



