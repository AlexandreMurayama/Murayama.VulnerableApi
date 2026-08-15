# Murayama Vulnerable API

An intentionally vulnerable ASP.NET Core Web API designed for hands-on study of the **OWASP API Security Top 10 — 2023**.

The project contains vulnerable and secure implementations of common API security flaws, allowing each vulnerability to be reproduced, analyzed and compared with its corresponding mitigation.

> ⚠️ **Educational project only.**
>
> This application intentionally contains security vulnerabilities.
> Do not expose it to the Internet or deploy it in a production environment.

---

## 🎯 Project Goals

Murayama Vulnerable API was created as a practical Application Security laboratory with the following goals:

- Study the OWASP API Security Top 10 through hands-on examples.
- Understand how API vulnerabilities appear in application code.
- Reproduce vulnerable behavior through HTTP requests.
- Implement secure alternatives to each vulnerable endpoint.
- Compare vulnerable and secure implementations.
- Document exploitation evidence and mitigations.
- Practice secure development using ASP.NET Core.
- Build a practical API Security / AppSec portfolio project.

---

## 🛡️ OWASP API Security Top 10 — 2023

This project implements laboratories covering all ten categories of the OWASP API Security Top 10 2023.

| # | Vulnerability | Laboratory | Documentation |
|---|---|---|---|
| API1 | Broken Object Level Authorization | Vulnerable vs ownership-aware order access | [View](docs/vulnerabilities/API1-BOLA.md) |
| API2 | Broken Authentication | Weak vs protected authentication flow | [View](docs/vulnerabilities/API2-Broken-Authentication.md) |
| API3 | Broken Object Property Level Authorization | Mass assignment / property authorization | [View](docs/vulnerabilities/API3-BOPLA.md) |
| API4 | Unrestricted Resource Consumption | Unbounded vs constrained resource requests | [View](docs/vulnerabilities/API4-Unrestricted-Resource-Consumption.md) |
| API5 | Broken Function Level Authorization | User access to administrative functionality | [View](docs/vulnerabilities/API5-Broken-Function-Level-Authorization.md) |
| API6 | Unrestricted Access to Sensitive Business Flows | Repeated coupon redemption | [View](docs/vulnerabilities/API6-Unrestricted-Access-to-Sensitive-Business-Flows.md) |
| API7 | Server-Side Request Forgery | User-controlled server-side requests | [View](docs/vulnerabilities/API7-Server-Side-Request-Forgery.md) |
| API8 | Security Misconfiguration | Internal error information disclosure | [View](docs/vulnerabilities/API8-Security-Misconfiguration.md) |
| API9 | Improper Inventory Management | Forgotten legacy API version | [View](docs/vulnerabilities/API9-Improper-Inventory-Management.md) |
| API10 | Unsafe Consumption of APIs | Unvalidated third-party API responses | [View](docs/vulnerabilities/API10-Unsafe-Consumption-of-APIs.md) |

---

## 🏗️ Architecture

The laboratory uses a deliberately simple architecture so that the security behavior remains easy to understand.

```text
                    HTTP Client
                        │
                        ▼
               ASP.NET Core Web API
                        │
          ┌─────────────┴─────────────┐
          │                           │
          ▼                           ▼
 Vulnerable Endpoints          Secure Endpoints
          │                           │
          └─────────────┬─────────────┘
                        │
                        ▼
                 Entity Framework
                        │
                        ▼
                    PostgreSQL
```

Some laboratories also contain simulated internal or third-party services:

```text
API7 SSRF
API ─────► simulated internal resource

API10 Unsafe Consumption
API ─────► simulated third-party API
```

All simulations remain inside the local laboratory environment.

---

## 🧰 Technology Stack

- **C#**
- **ASP.NET Core Web API**
- **.NET**
- **Entity Framework Core**
- **PostgreSQL**
- **Docker**
- **JWT Bearer Authentication**
- **Role-Based Authorization**
- **HttpClient / IHttpClientFactory**
- **EF Core Migrations**
- **HTTP request files (`.http`)**

---

## 🔐 Authentication

The API uses JWT Bearer authentication.

Example:

```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "alice@murayama.local",
  "password": "Alice123!"
}
```

The returned access token can then be supplied using:

```http
Authorization: Bearer <ACCESS_TOKEN>
```

> Never commit real authentication tokens to the repository.

---

## 🧪 Laboratory Design

Most vulnerabilities follow the same structure:

```text
Vulnerable implementation
          │
          ▼
Reproduce vulnerability
          │
          ▼
Capture evidence
          │
          ▼
Secure implementation
          │
          ▼
Verify mitigation
```

Endpoints are generally separated using:

```text
/api/vulnerable/...
```

and:

```text
/api/secure/...
```

This makes it possible to compare insecure and secure implementations side by side.

---

## 🔎 Example — Broken Object Level Authorization

A vulnerable endpoint may retrieve an order directly from a client-controlled identifier:

```text
GET /api/vulnerable/orders/2
```

without verifying ownership.

Conceptually:

```text
Alice
  │
  │ requests Order 2
  ▼
API
  │
  ▼
Order exists?
  │
  └── Yes → Return order ❌
```

The secure implementation additionally verifies that the authenticated user owns the requested object:

```text
Alice
  │
  │ requests Order 2
  ▼
API
  │
  ▼
Order belongs to Alice?
  │
  ├── Yes → Return
  └── No  → Reject ✅
```

See the complete laboratory documentation in:

[API1 — Broken Object Level Authorization](docs/vulnerabilities/API1-BOLA.md)

---

## 📂 Project Structure

```text
.
├── Murayama.VulnerableApi/
│   ├── Controllers/
│   ├── Data/
│   ├── DTOs/
│   ├── Migrations/
│   ├── Models/
│   ├── Services/
│   ├── Settings/
│   ├── Murayama.VulnerableApi.csproj
│   └── Murayama.VulnerableApi.http
│
├── docs/
│   ├── images/
│   │   ├── api1-bola/
│   │   ├── api2-broken-authentication/
│   │   ├── api3-bopla/
│   │   ├── api4-unrestricted-resource-consumption/
│   │   ├── api5-bfla/
│   │   ├── api6-sensitive-business-flows/
│   │   ├── api7-ssrf/
│   │   ├── api8-security-misconfiguration/
│   │   ├── api9-improper-inventory-management/
│   │   └── api10-unsafe-consumption-of-apis/
│   │
│   └── vulnerabilities/
│       ├── API1-BOLA.md
│       ├── API2-Broken-Authentication.md
│       ├── API3-BOPLA.md
│       ├── API4-Unrestricted-Resource-Consumption.md
│       ├── API5-Broken-Function-Level-Authorization.md
│       ├── API6-Unrestricted-Access-to-Sensitive-Business-Flows.md
│       ├── API7-Server-Side-Request-Forgery.md
│       ├── API8-Security-Misconfiguration.md
│       ├── API9-Improper-Inventory-Management.md
│       └── API10-Unsafe-Consumption-of-APIs.md
│
└── README.md
```

---

## 🚀 Running the Laboratory

### Requirements

Install:

- .NET SDK
- Docker
- PostgreSQL container through Docker
- Git

Verify .NET:

```bash
dotnet --version
```

Verify Docker:

```bash
docker --version
```

---

### Start PostgreSQL

Start the PostgreSQL container used by the laboratory.

Verify that it is running:

```bash
docker ps
```

---

### Apply Database Migrations

Navigate to the directory containing:

```text
Murayama.VulnerableApi.csproj
```

and run:

```bash
dotnet ef database update
```

---

### Run the API

From the same project directory:

```bash
dotnet run
```

The local development instance used throughout the laboratory is typically available at:

```text
http://localhost:5248
```

The exact port may differ depending on the local development configuration.

---

### Health Check

```http
GET http://localhost:5248/api/health
```

Expected response:

```http
HTTP/1.1 200 OK
```

---

## 🧪 HTTP Tests

The repository contains:

```text
Murayama.VulnerableApi.http
```

This file contains requests used to reproduce the laboratories.

Examples include:

```http
GET {{host}}/api/vulnerable/orders/2
Authorization: Bearer {{token}}
```

and their secure counterparts:

```http
GET {{host}}/api/secure/orders/2
Authorization: Bearer {{token}}
```

The requests can be executed using IDEs with `.http` file support.

---

## 🗄️ Database

PostgreSQL is used to persist laboratory data.

Entity Framework Core handles:

- entity mapping
- database access
- schema migrations
- relationships
- database constraints

Some security controls are intentionally enforced at the database layer as well.

For example, the API6 laboratory uses a unique constraint to prevent multiple coupon redemptions for the same user:

```text
UNIQUE(UserId, CouponCode)
```

This demonstrates defense in depth between application validation and database integrity.

---

## 🔒 Security Techniques Demonstrated

The secure implementations demonstrate techniques including:

- Object-level authorization
- Property-level authorization
- Role-based access control
- JWT authentication
- DTO-based input models
- Server-side business-rule validation
- Database uniqueness constraints
- Resource limits
- SSRF destination validation
- Private and loopback address blocking
- Secure error handling
- Server-side exception logging
- API lifecycle management
- External API response validation
- HTTP timeouts
- Upstream failure handling

---

## 📸 Evidence

Each laboratory contains screenshots demonstrating vulnerable and secure behavior.

Evidence is stored under:

```text
docs/images/
```

Examples include:

- vulnerable HTTP responses
- secure HTTP responses
- authorization failures
- PostgreSQL verification
- server-side logs
- simulated internal services
- simulated third-party API responses

The detailed reports reference these images directly.

---

## ⚠️ Security Warning

This repository intentionally contains vulnerable code.

It is designed for:

- cybersecurity education
- Application Security study
- secure coding practice
- API security testing
- local laboratory exercises

It is **not** designed for:

- production deployment
- Internet-facing hosting
- processing real user information
- storing real credentials
- handling sensitive information

Run the application only in an isolated and controlled environment.

---

## 📚 References

- [OWASP API Security Project](https://owasp.org/www-project-api-security/)
- [OWASP API Security Top 10 — 2023](https://owasp.org/API-Security/editions/2023/en/0x11-t10/)
- [OWASP Cheat Sheet Series](https://cheatsheetseries.owasp.org/)
- [Microsoft ASP.NET Core Documentation](https://learn.microsoft.com/aspnet/core/)
- [Entity Framework Core Documentation](https://learn.microsoft.com/ef/core/)

---

## 👤 Author

**Alexandre Murayama**

Software Developer transitioning into Cybersecurity, with a focus on:

- Application Security
- API Security
- Penetration Testing
- Secure Software Development

---

## 📄 License

This project is intended for educational and cybersecurity training purposes.

See the repository license for usage terms.