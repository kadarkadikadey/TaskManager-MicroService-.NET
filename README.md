# 🚀 Task Manager Microservice

A professional-grade **RESTful API** built with **.NET 8** and **MongoDB Atlas**. This project demonstrates a clean microservices architecture, implementing the Service pattern to manage task lifecycles for platforms like **CatSoftShop** or **Citizen 107**.

---

## 🛠️ Tech Stack

* **Backend:** .NET 8 (Minimal APIs)
* **Database:** MongoDB Atlas (NoSQL)
* **Tooling:** Docker, Swagger UI, REST Client
* **Language:** C#

---

## ✨ Key Features

* **Full CRUD Lifecycle:** Create, Retrieve, Update, and Delete tasks with real-time MongoDB synchronization.
* **Resilient Data Mapping:** Uses `BsonIgnoreExtraElements` and `BsonRepresentation` to ensure the service doesn't crash if extra fields (like `DueDate`) exist in the database.
* **Business Logic Layer:** Decoupled architecture using a dedicated `TaskService` to handle data validation and processing.
* **API Documentation:** Fully integrated **Swagger UI** for interactive testing and API exploration.

---

## 📂 Project Structure

```text
Task.Catalog.Service/
├── Models/          # TaskItem entity with BSON attributes
├── Services/        # MongoDB orchestration and business logic
├── Properties/      # Launch settings and configuration
├── Program.cs       # DI Container, Middleware, and Routes
└── .http            # VS Code REST Client testing file
```
## ⚙️ Getting Started

Follow these steps to get your local development environment running.

### 1. Prerequisites
* **.NET 8 SDK:** [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
* **MongoDB Atlas:** A free cluster and a Database User.
* **VS Code Extensions:** C# Dev Kit and REST Client (recommended).

### 2. Database Configuration
Create an `appsettings.json` file in the `Task.Catalog.Service` root folder:

```json
{
  "ConnectionStrings": {
    "MongoDb": "your_mongodb_atlas_connection_string"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```
### 3. Verification

Once the app is running (usually on `http://localhost:5217`):

* **Swagger Documentation:** Open [http://localhost:5217/swagger](http://localhost:5217/swagger) in your browser to interact with the API endpoints.
* **Health Check:** Send a `GET` request to `/api/tasks`. A successful `200 OK` response confirms your MongoDB Atlas connection is active and the service is healthy.

---

## 👨‍💻 Author

**Kartikey**
* **Role:** Full-Stack Developer | .NET & Flutter Enthusiast
