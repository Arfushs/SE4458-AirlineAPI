# Airline Ticketing System - Web API (SE4458 Midterm)

## ✈️ Overview

This is a **.NET 8 Web API project** developed as a midterm for the SE4458 Software Architecture & Design course. It simulates an **Airline Ticketing System**, where users can:

- Add flights
- Query available flights
- Buy tickets
- Check in
- View flight passenger lists

The backend uses **SQLite** with **EF Core** and supports **JWT-based authentication** for protected endpoints. The application is deployed via **Azure App Service**.

---

## 🔗 Live Demo

**Swagger UI:**\
[https://airline-api-awfub5eterejhshk.germanywestcentral-01.azurewebsites.net/swagger](https://airline-api-awfub5eterejhshk.germanywestcentral-01.azurewebsites.net/swagger/index.html)

**Video Walkthrough:**\
▶️ [https://drive.google.com/file/d/xyz123/view](https://drive.google.com/file/d/xyz123/view) 

---

## 🔧 Technologies

- ASP.NET Core 8 Web API
- SQLite & EF Core
- Azure App Service
- JWT Authentication
- Swagger (OpenAPI)
- Rider IDE

---

## 🔍 Endpoints

### ✈️ Flight Controller

| Endpoint             | Auth Required | Paging | Description                 |
| -------------------- | ------------- | ------ | --------------------------- |
| `POST /Flight/add`   | Yes           | No     | Adds a flight to the system |
| `GET  /Flight/query` | No            | Yes    | Queries available flights   |

### 🎩 Ticket Controller

| Endpoint                  | Auth Required | Paging | Description                      |
| ------------------------- | ------------- | ------ | -------------------------------- |
| `POST /Ticket/buy`        | Yes           | No     | Buys a ticket for a flight       |
| `PUT  /Ticket/checkin`    | No            | No     | Checks in a passenger            |
| `GET  /Ticket/passengers` | Yes           | Yes    | Lists all passengers of a flight |

### 🔐 Auth Controller

| Endpoint           | Auth Required | Description         |
| ------------------ | ------------- | ------------------- |
| `POST /Auth/login` | No            | Returns a JWT token |

---

## 🖐 Test Order

1. `POST /Auth/login` - Get token
2. `POST /Flight/add` - Create flight (Auth)
3. `GET /Flight/query` - Search flights
4. `POST /Ticket/buy` - Buy ticket (Auth)
5. `PUT /Ticket/checkin` - Check-in
6. `GET /Ticket/passengers` - View passengers (Auth)

---

## 🖋️ Entity-Relationship Diagram (ERD)



---

## ❌ Problems Faced

- Azure region policies blocked deployment in default region
  - ✅ Fixed by switching to `germanywestcentral`
- Internal Server Errors without logs
  - ✅ Solved by enabling EF Core migrations & proper DB init
- Check-in not working initially
  - ✅ Root cause: ticket match logic, improved error handling
- QueryFlights was listing sold out flights
  - ✅ Fixed by decreasing capacity when buying ticket
- Swagger testing sequence confusion
  - ✅ Resolved with clean ordering and token header setup

---

## 🚀 How to Run Locally

1. Clone the repo
2. Add `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=airline.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

3. Run migrations (optional)

```bash
dotnet ef database update
```

4. Run app

```bash
dotnet run
```

5. Go to: `https://localhost:5001/swagger`

---

## 🙌 Credits

Developed by: **Can Berk Soydan**\
Course: SE4458 

