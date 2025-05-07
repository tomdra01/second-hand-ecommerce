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

## ⚡ Redis Caching Explained

To improve performance, we've integrated Redis as a caching layer for `GET /api/itemlistings`.

### How it works:
- On the **first request**, the system fetches all listings from **MongoDB** and stores them in **Redis**.
- Subsequent requests are served directly from **Redis**, significantly reducing latency.

### 🔁 Cache Invalidation:
- Whenever a new item listing is **created**, the cache is **invalidated** to ensure data consistency.

### ⏱ Real Performance Gain:
| Source      | Response Time |
|-------------|----------------|
| MongoDB     | ~110–240 ms    |
| Redis Cache | ~4–28 ms       |

- MongoDB response times can vary based on the number of items in the database. The more items, the longer it takes to fetch them.

- First request to MongoDB takes longer due to the initial fetch. 

- Redis response times are consistent and significantly faster, regardless of the number of items. 

> Result: ~10× faster response times using Redis! 🧠🚀

## 🖼️ Image Upload & Redis Caching

### 📸 Image Upload with MinIO

When creating a new item listing via `POST /api/itemlistings`, the API accepts a `multipart/form-data` request including:

- Title
- Description
- Price
- SellerId
- Image (file upload)

Uploaded images are stored in a MinIO bucket (`item-images`), and the public URL is returned in the response and stored in MongoDB under the `ImageUrls` field.

#### ✅ Example Response

```json
{
  "message": "Listing created successfully",
  "listing": {
    "title": "Macbook Pro 2023",
    "description": "Top condition",
    "price": 800.0,
    "sellerId": "user222",
    "imageUrls": [
      "http://localhost:9000/item-images/2a63646d-6178-4fbc-9e16-e6067a02624d_logo.png"
    ]
  }
}
```

# Second-Hand E-Commerce Backend

## Technologies Used
- ASP.NET Core (.NET 9)
- MongoDB (NoSQL)
- Redis (Caching)
- MinIO (Cloud Storage)
- CQRS (Commands & Queries)
- Docker + Docker Compose
- Serilog (Logging)

## Architecture
- Follows Clean Architecture principles.
- API is decoupled from Application and Infrastructure layers.

## Database Strategy
- MongoDB for listings, orders, and users due to flexible schema.
- Redis for caching frequently accessed listings.

## Cloud Storage
- MinIO is used for storing uploaded item images.
- Images are uploaded using `IFileStorageService` abstraction.

## CQRS
- Commands: CreateItemListingCommand
- Queries: GetAllItemListings, GetItemListingById
- Handlers isolate read/write responsibilities.

## Transactions
- (Explain your planned or implemented MongoDB multi-document transaction here — even for a fake "PlaceOrder" flow.)

## Group Contributions
- [Your name] – CQRS, Mongo setup, caching
- [Other name] – Cloud storage, testing, Docker

## Running Locally
- `docker-compose up`
- `dotnet run` in `/src/API`

## Future Improvements
- Add search functionality
- Full authentication and seller verification




