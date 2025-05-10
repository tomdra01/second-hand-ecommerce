# 🛍️ Second-Hand E-Commerce Backend

This repository contains the backend implementation of a second-hand e-commerce platform, developed for the **"Databases for Developers"** course. The project showcases practical application of modern backend design including NoSQL databases, CQRS, caching, cloud storage, and transactional operations.

---

## 🧑‍💻 Group Contributions

**Solo project** by **Tomas Dracka** — all design, development, testing, and documentation were done independently.

---

## 📦 Technologies Used

- **.NET 8** – Modern backend framework
- **MongoDB** – NoSQL database
- **Redis** – High-performance caching
- **MinIO** – S3-compatible cloud storage
- **Docker + Docker Compose** – Containerized deployment
- **CQRS** – Clean separation of commands and queries
- **Serilog** – Structured logging

---

## 🧱 Architecture Overview

This solution follows **Clean Architecture**, structured as:

- `Domain` – Core entities (ItemListing, Order, Review, UserProfile)
- `Application` – CQRS handlers, service interfaces, and DTOs
- `Infrastructure` – MongoDB repositories, Redis cache, MinIO storage
- `API` – Controllers, DI setup, Swagger UI

Each layer is strictly separated to promote maintainability, testability, and scalability.

---

## 🗃️ Database Selection

### 🟢 MongoDB (NoSQL)

Chosen for its flexibility and scalability:
- Ideal for semi-structured data like item listings and reviews
- Dynamic schema lets us evolve features without migrations
- Natively supports transactions for multi-document updates

No relational database was used since no complex relational joins were required.

---

## 🧩 Data Schema

### ItemListing
```json
{
  "id": "guid",
  "title": "string",
  "description": "string",
  "price": "decimal",
  "sellerId": "string",
  "imageUrls": ["string"],
  "isSold": "bool"
}
```

### Order
```json
{
  "id": "guid",
  "itemId": "guid",
  "buyerId": "string",
  "totalPrice": "decimal",
  "placedAt": "datetime"
}
```

### Review
```json
{
  "sellerId": "string",
  "reviewerId": "string",
  "rating": "int",
  "comment": "string",
  "createdAt": "datetime"
}
```

### UserProfile
```json
{
  "id": "guid",
  "username": "string",
  "email": "string",
  "bio": "string"
}
```

---

## ☁️ Cloud Storage (MinIO)

MinIO handles scalable, S3-compatible image storage:
- `IFormFile` is uploaded in the `CreateItemListingHandler`
- Stored in MinIO via `MinioStorageService`
- Public image URLs are saved alongside listings in MongoDB

Why MinIO? It’s lightweight, self-hostable, and mimics AWS S3 for dev/local use.

---

## 🚀 Caching Strategy

### 🔴 Redis

Used to boost performance on frequently accessed endpoints:
- Caches `GetAllItemListings`, `GetAllOrders`, `GetUserProfiles`, etc.
- Implements **manual cache invalidation** in relevant create/update handlers

Cache Keys:
- `item_listings_all`
- `orders_all`
- `reviews_all`
- `user_profiles_all`

---

## ⭐ User Review & Profile Features

### Reviews:
- Users can submit reviews for sellers after purchases
- Includes rating, comment, and timestamp
- Cached for fast access per seller or globally

### User Profiles:
- Public-facing profiles with username, email, and optional bio
- Fully implemented with CQRS, MongoDB, and caching

---

## 🔀 CQRS Pattern

All write (command) and read (query) operations are split cleanly into separate handlers.

### Commands:
- `CreateItemListingCommand`
- `CreateOrderCommand`
- `CreateReviewCommand`
- `CreateUserProfileCommand`

### Queries:
- `GetAllItemListingsQuery`
- `GetItemListingByIdQuery`
- `GetAllOrdersQuery`
- `GetOrderByIdQuery`
- `GetAllReviewsQuery`
- `GetReviewsBySellerIdQuery`
- `GetAllUserProfilesQuery`

Why CQRS? It increases modularity, supports scaling reads/writes independently, and improves clarity in business logic.

---

## 🔁 Transaction Management

MongoDB supports **multi-document transactions**, used especially in order placement:

### Example Flow:
1. Create a new Order
2. Mark the associated ItemListing as `IsSold = true`
3. Run in a MongoDB transaction — ensures atomicity

If anything fails, all operations are rolled back.

---

## 🧪 Testing Strategy

- API tested via **Postman** collections
- Verified all CRUD operations: Listings, Orders, Reviews, UserProfiles
- Checked image upload and public URL generation via MinIO
- Console/debug logs validated using **Serilog**

---

## 🧠 Design Decisions

- MongoDB chosen over SQL for flexibility and scaling unstructured data
- Redis improves speed and user experience on read-heavy endpoints
- CQRS allows clean separation of business logic and better test coverage
- MinIO gives real-world cloud storage simulation without AWS lock-in
- Docker ensures platform independence and fast deployment

---

## ▶️ Running the App

```bash
docker compose up --build -d
```

---

## 📚 Summary

This backend solution addresses all core requirements of the assignment:

- ✅ **List items**
- ✅ **Browse listings**
- ✅ **Place orders with transactions**
- ✅ **Review sellers**
- ✅ **Create and fetch user profiles**
- ✅ **Cloud image storage via MinIO**
- ✅ **Caching with Redis**
- ✅ **CQRS pattern in all services**
- ✅ **MongoDB transaction support**
- ✅ **Documented design assumptions**
- ✅ **Solo implementation**

---

## 📎 License

Educational use only.