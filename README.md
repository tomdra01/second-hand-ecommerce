# Second-Hand E-Commerce Backend

This repository contains the backend implementation of a second-hand e-commerce platform developed as part of the "Databases for Developers" course assignment. The system demonstrates modern architectural and database practices including NoSQL storage, CQRS, caching, cloud storage, and transaction handling.

## 🧑‍💻 Group Contributions
- **Tomas Dracka** 


---

## 📦 Technologies Used

- **.NET 8**
- **MongoDB** (NoSQL)
- **Redis** (Caching)
- **MinIO** (Cloud file storage)
- **Docker + Docker Compose**
- **CQRS pattern**
- **Serilog** (logging)

---

## 🧱 Architecture Overview

The backend follows Clean Architecture principles with the following projects:
- `Domain`: Entity definitions
- `Application`: CQRS handlers, service interfaces, DTOs
- `Infrastructure`: MongoDB repositories, Redis/MinIO services
- `API`: Controllers, Swagger, dependency injection

---

## 🗃️ Database Selection

### MongoDB
- Used as the primary NoSQL database to store dynamic, flexible item listings, orders, and user-related data.
- Enables schema-less design, scalability, and easy integration with Docker.

---

## 🧩 Data Models

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
  "quantity": "int",
  "totalPrice": "decimal",
  "placedAt": "datetime"
}
```

---

## ☁️ Cloud Storage

MinIO is used to store uploaded images for item listings. The process is:
- Images are uploaded using `IFormFile` in the `CreateItemListingHandler`.
- Files are stored in MinIO using `MinioStorageService`.
- Public URLs are generated and stored alongside item listings in MongoDB.

---

## 🚀 Caching Strategy

- Redis is used to cache frequently accessed data such as item listings and orders.
- `GetAllItemListings` and `GetAllOrders` use Redis to serve from cache.
- `CreateItemListing` and `CreateOrder` handlers **invalidate cache** by removing keys (`item_listings_all`, `orders_all`).

---

## 🔀 CQRS Implementation

We use separate command/query handlers for improved separation of concerns.

### Commands:
- `CreateItemListingCommand`
- `CreateOrderCommand`

### Queries:
- `GetAllItemListingsQuery`
- `GetItemListingByIdQuery`
- `GetAllOrdersQuery`
- `GetOrderByIdQuery`

---

## 🔁 Transactions

MongoDB multi-document transactions ensure consistency for critical operations:

### Example – Placing an Order:
- Insert `Order` document into `Orders` collection.
- Mark corresponding `ItemListing` as `IsSold = true`.
- If either fails, transaction is rolled back.

---

## 🧪 Testing

- Endpoints tested using **Postman** (Create, GetAll, GetById for listings and orders).
- Console logs monitored via Serilog for debugging and request tracing.

---

## 📝 Design Decisions & Assumptions

- Used MongoDB due to schema flexibility for item listings and easy support for rich data types.
- Redis was selected for its speed and ease of integration with .NET for caching.
- CQRS helps decouple read/write logic and makes handlers independently testable.
- MinIO allows us to simulate cloud-based S3-compatible file storage in local/dev environments.
- Transactions are used for order placement to maintain data consistency.

---

## 📄 How to Run

```bash
docker compose up --build -d
```



## 📎 License

This project is open for educational purposes.