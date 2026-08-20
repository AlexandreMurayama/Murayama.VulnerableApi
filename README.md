# Murayama Vulnerable API

[**English**](README.md) | [Português (Brasil)](README-PT-BR.md)

![.NET](https://img.shields.io/badge/.NET-ASP.NET_Core-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![C%23](https://img.shields.io/badge/C%23-Language-512BD4?style=flat-square&logo=csharp&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Database-4169E1?style=flat-square&logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-Containerized-2496ED?style=flat-square&logo=docker&logoColor=white)
![OWASP](https://img.shields.io/badge/OWASP-API_Security_Top_10-000000?style=flat-square&logo=owasp&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-yellow?style=flat-square)
[![CI - Build and Test](https://github.com/AlexandreMurayama/Murayama.VulnerableApi/actions/workflows/ci.yml/badge.svg)](https://github.com/AlexandreMurayama/Murayama.VulnerableApi/actions/workflows/ci.yml)
[![CodeQL Security Analysis](https://github.com/AlexandreMurayama/Murayama.VulnerableApi/actions/workflows/codeql.yml/badge.svg)](https://github.com/AlexandreMurayama/Murayama.VulnerableApi/actions/workflows/codeql.yml)

An intentionally vulnerable ASP.NET Core Web API designed for hands-on
study of the **OWASP API Security Top 10 --- 2023**.

The project contains vulnerable and secure implementations of common API
security flaws, allowing each vulnerability to be reproduced, analyzed
and compared with its corresponding mitigation.

> ⚠️ **Educational project only.**
>
> This application intentionally contains security vulnerabilities. Do
> not expose it to the Internet or deploy it in a production
> environment.

------------------------------------------------------------------------

## 🎯 Project Goals

Murayama Vulnerable API was created as a practical Application Security
laboratory with the following goals:

-   Study the OWASP API Security Top 10 through hands-on examples.
-   Understand how API vulnerabilities appear in application code.
-   Reproduce vulnerable behavior through HTTP requests.
-   Implement secure alternatives to each vulnerable endpoint.
-   Compare vulnerable and secure implementations.
-   Document exploitation evidence and mitigations.
-   Practice secure development using ASP.NET Core.
-   Build a practical API Security / AppSec portfolio project.
-   Practice automated security testing with integration tests.
-   Implement a practical DevSecOps pipeline with CI, SAST and dependency security controls.

------------------------------------------------------------------------

## 🛡️ OWASP API Security Top 10 --- 2023

This project implements laboratories covering all ten categories of the
OWASP API Security Top 10 2023.

  ---------------------------------------------------------------------------------------------------------------------------------------------
  \#                Vulnerability      Laboratory        Documentation
  ----------------- ------------------ ----------------- --------------------------------------------------------------------------------------
  API1              Broken Object      Vulnerable vs     [View](docs/vulnerabilities/API1-BOLA.md)
                    Level              ownership-aware   
                    Authorization      order access      

  API2              Broken             Weak vs protected [View](docs/vulnerabilities/API2-Broken-Authentication.md)
                    Authentication     authentication    
                                       flow              

  API3              Broken Object      Mass assignment / [View](docs/vulnerabilities/API3-BOPLA.md)
                    Property Level     property          
                    Authorization      authorization     

  API4              Unrestricted       Unbounded vs      [View](docs/vulnerabilities/API4-Unrestricted-Resource-Consumption.md)
                    Resource           constrained       
                    Consumption        resource requests 

  API5              Broken Function    User access to    [View](docs/vulnerabilities/API5-Broken-Function-Level-Authorization.md)
                    Level              administrative    
                    Authorization      functionality     

  API6              Unrestricted       Repeated coupon   [View](docs/vulnerabilities/API6-Unrestricted-Access-to-Sensitive-Business-Flows.md)
                    Access to          redemption        
                    Sensitive Business                   
                    Flows                                

  API7              Server-Side        User-controlled   [View](docs/vulnerabilities/API7-Server-Side-Request-Forgery.md)
                    Request Forgery    server-side       
                                       requests          

  API8              Security           Internal error    [View](docs/vulnerabilities/API8-Security-Misconfiguration.md)
                    Misconfiguration   information       
                                       disclosure        

  API9              Improper Inventory Forgotten legacy  [View](docs/vulnerabilities/API9-Improper-Inventory-Management.md)
                    Management         API version       

  API10             Unsafe Consumption Unvalidated       [View](docs/vulnerabilities/API10-Unsafe-Consumption-of-APIs.md)
                    of APIs            third-party API   
                                       responses         
  ---------------------------------------------------------------------------------------------------------------------------------------------

------------------------------------------------------------------------

## 🏗️ Architecture

The laboratory uses a deliberately simple architecture so that the
security behavior remains easy to understand.

``` text
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

Some laboratories also contain simulated internal or third-party
services:

``` text
API7 SSRF
API ─────► simulated internal resource

API10 Unsafe Consumption
API ─────► simulated third-party API
```

All simulations remain inside the local laboratory environment.

------------------------------------------------------------------------

## 🧰 Technology Stack

-   **C#**
-   **ASP.NET Core Web API**
-   **.NET**
-   **Entity Framework Core**
-   **PostgreSQL**
-   **Docker**
-   **JWT Bearer Authentication**
-   **Role-Based Authorization**
-   **HttpClient / IHttpClientFactory**
-   **EF Core Migrations**
-   **HTTP request files (`.http`)**
-   **xUnit**
-   **ASP.NET Core WebApplicationFactory**
-   **GitHub Actions**
-   **GitHub CodeQL**

------------------------------------------------------------------------

## 👥 Lab Credentials

The application automatically seeds three fictitious users for
laboratory purposes.

  User    Email                    Password      Role
  ------- ------------------------ ------------- -------
  Alice   `alice@murayama.local`   `Alice123!`   User
  Bob     `bob@murayama.local`     `Bob123!`     User
  Admin   `admin@murayama.local`   `Admin123!`   Admin

> ⚠️ These credentials are intentionally public and exist exclusively
> for the local security laboratory. They must never be reused for real
> accounts or production environments.

The different accounts are used to demonstrate authentication and
authorization vulnerabilities such as:

-   Broken Object Level Authorization (BOLA)
-   Broken Authentication
-   Broken Object Property Level Authorization (BOPLA)
-   Broken Function Level Authorization (BFLA)
-   Role-based access control

For example, authenticate as Alice:

``` http
POST /api/auth/login
Content-Type: application/json

{
  "email": "alice@murayama.local",
  "password": "Alice123!"
}
```

Then use the returned JWT:

``` http
Authorization: Bearer <ACCESS_TOKEN>
```

Never commit generated JWT access tokens to the repository.

------------------------------------------------------------------------

## 🔐 Authentication

The API uses JWT Bearer authentication.

After authenticating with one of the laboratory accounts, the API
returns an access token.

Authenticated requests must provide the token using:

``` http
Authorization: Bearer <ACCESS_TOKEN>
```

JWT signing keys are supplied through local configuration and are not
committed to the repository.

> Never commit generated authentication tokens or signing keys to the
> repository.

------------------------------------------------------------------------

## 🧪 Laboratory Design

Most vulnerabilities follow the same structure:

``` text
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

``` text
/api/vulnerable/...
```

and:

``` text
/api/secure/...
```

This makes it possible to compare insecure and secure implementations
side by side.

------------------------------------------------------------------------

## 🔎 Example --- Broken Object Level Authorization

A vulnerable endpoint may retrieve an order directly from a
client-controlled identifier:

``` text
GET /api/vulnerable/orders/2
```

without verifying ownership.

Conceptually:

``` text
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

The secure implementation additionally verifies that the authenticated
user owns the requested object:

``` text
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

[API1 --- Broken Object Level
Authorization](docs/vulnerabilities/API1-BOLA.md)

------------------------------------------------------------------------

## 📂 Project Structure

``` text
.
├── .github/
│   └── workflows/
│       ├── ci.yml
│       ├── codeql.yml
│       └── dependency-review.yml
│
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
├── Murayama.VulnerableApi.Tests/
│   ├── Infrastructure/
│   │   ├── CustomWebApplicationFactory.cs
│   │   ├── RecordingHttpClientFactory.cs
│   │   ├── RecordingHttpMessageHandler.cs
│   │   └── SsrfWebApplicationFactory.cs
│   ├── Integration/
│   │   └── HealthCheckTests.cs
│   ├── Security/
│   │   ├── BolaTests.cs
│   │   ├── BoplaTests.cs
│   │   ├── BrokenAuthenticationTests.cs
│   │   └── SsrfTests.cs
│   ├── AssemblyInfo.cs
│   └── Murayama.VulnerableApi.Tests.csproj
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
├── .env.example
├── compose.yaml
├── LICENSE
└── README.md
```

------------------------------------------------------------------------

## 🚀 Running the Laboratory

### Requirements

Install:

-   .NET SDK
-   Docker
-   Git

Verify .NET:

``` bash
dotnet --version
```

Verify Docker:

``` bash
docker --version
```

### Environment Configuration

Create a local `.env` file based on `.env.example`.

Example:

``` dotenv
POSTGRES_DB=murayama_vulnerable_api
POSTGRES_USER=your_postgres_user
POSTGRES_PASSWORD=your_postgres_password

JWT_KEY=replace_with_a_development_jwt_signing_key
```

The `.env` file is ignored by Git and must not be committed.

### Run with Docker Compose

``` bash
docker compose up --build -d
```

Verify the containers:

``` bash
docker ps
```

The Docker Compose configuration binds the laboratory services to the
local machine.

The API is available at:

``` text
http://localhost:8080
```

### Health Check

``` http
GET http://localhost:8080/api/health
```

Expected response:

``` http
HTTP/1.1 200 OK
```

### Local .NET Execution

When running directly through the .NET development environment, apply
the database migrations from the directory containing
`Murayama.VulnerableApi.csproj`:

``` bash
dotnet ef database update
```

Then:

``` bash
dotnet run
```

The local development instance used throughout the laboratory is
typically available at:

``` text
http://localhost:5248
```

The exact port may differ depending on the local development
configuration.

------------------------------------------------------------------------

## 🧪 HTTP Tests

The repository contains:

``` text
Murayama.VulnerableApi.http
```

This file contains requests used to reproduce the laboratories.

Examples include:

``` http
GET {{host}}/api/vulnerable/orders/2
Authorization: Bearer {{token}}
```

and their secure counterparts:

``` http
GET {{host}}/api/secure/orders/2
Authorization: Bearer {{token}}
```

The requests can be executed using IDEs with `.http` file support.

------------------------------------------------------------------------

## ⚙️ DevSecOps & Automated Security Testing

The project includes a DevSecOps pipeline designed to automatically validate
the application, its dependencies and selected security behaviors.

### Continuous Integration

GitHub Actions automatically executes the CI pipeline on pushes and pull
requests targeting the `main` branch.

The pipeline performs:

``` text
Pull Request / Push
        │
        ▼
Restore Dependencies
        │
        ▼
Build (.NET 10)
        │
        ▼
PostgreSQL 17 Test Environment
        │
        ▼
Automated Tests
        │
        ├── Integration Tests
        └── Security Tests
```

A dedicated PostgreSQL 17 service container is created by GitHub Actions for
the automated test environment. Database migrations and seed data are applied
to this isolated database before the tests execute.

### Automated Security Tests

Selected OWASP API Security scenarios are reproduced through automated
integration tests using **xUnit** and **WebApplicationFactory**.

| OWASP | Security Test | Behavior Validated |
|---|---|---|
| API1:2023 | BOLA | An authenticated user can access an object belonging to another user |
| API2:2023 | Broken Authentication | Repeated failed authentication attempts are accepted without rate limiting |
| API3:2023 | BOPLA | A regular user can modify the sensitive `Role` property |
| API7:2023 | SSRF | A user-controlled loopback URL reaches the server-side HTTP client |

> **Important:** This is an intentionally vulnerable application.
>
> A passing security test does **not** mean that the vulnerable endpoint is
> secure. These tests act as executable specifications of the educational
> laboratory: they verify that the expected vulnerable behavior can still be
> reproduced.
>
> The secure counterparts remain documented separately and demonstrate the
> corresponding mitigations.

The SSRF test uses a controlled `HttpMessageHandler` to verify the destination
requested by the application without performing a real connection to an
internal or loopback service.

### Static Application Security Testing (SAST)

The repository uses **GitHub CodeQL** to perform static security analysis of
the C# codebase.

CodeQL analysis runs automatically through GitHub Actions and participates in
the repository security workflow.

``` text
Source Code
    │
    ▼
CodeQL Analysis
    │
    ▼
Security Findings
    │
    ▼
GitHub Code Scanning
```

### Dependency Security

**Dependency Review** runs on pull requests to identify potentially vulnerable
or risky dependency changes before they are merged.

**Dependabot** is enabled to monitor project dependencies and surface available
security and dependency updates.

### Secret Protection

The repository uses GitHub security controls to reduce the risk of credentials
being committed accidentally:

-   Secret Scanning
-   Push Protection

Real database passwords, JWT signing keys and generated access tokens are not
stored in the repository.

### Branch Protection and Security Gates

The `main` branch is protected by required pull request checks.

Changes are validated through automated CI and security analysis before being
merged.

The repository currently uses the following GitHub Actions workflows:

``` text
.github/workflows/
├── ci.yml
├── codeql.yml
└── dependency-review.yml
```

Together, these controls demonstrate a basic secure software delivery
lifecycle combining automated build validation, integration testing,
security testing, SAST, dependency analysis and secret protection.

------------------------------------------------------------------------


## 🔐 Security Assessment / Pentest

After the API laboratories and automated security controls were implemented, the
application was subjected to an authorized penetration test from a Kali Linux
environment.

The assessment started with a **black-box** approach using service discovery,
HTTP reconnaissance and endpoint enumeration. After the API specification was
identified locally, the assessment continued as **gray-box testing** to provide
systematic coverage of the exposed attack surface.

The pentest covered:

-   Service and endpoint discovery with Nmap, FFUF and curl
-   Authentication and JWT analysis
-   Broken Object Level Authorization (BOLA / IDOR)
-   Broken Object Property Level Authorization (BOPLA)
-   Broken Function Level Authorization (BFLA)
-   Server-Side Request Forgery (SSRF)
-   Sensitive property exposure
-   Security misconfiguration and verbose error handling
-   Excessive pagination and resource controls
-   Unsafe consumption of third-party API data
-   Authentication rate limiting and automated-abuse controls

### Pentest Results

The assessment confirmed **10 security findings**.

| ID | Finding | Proposed CVSS v3.1 |
|---|---|---:|
| F-01 | Server-Side Request Forgery (SSRF) | 8.1 High |
| F-02 | Broken Object Level Authorization (BOLA / IDOR) | 6.5 Medium |
| F-03 | BOPLA / Mass Assignment leading to Privilege Escalation | 8.8 High |
| F-04 | Sensitive Property Exposure | 4.3 Medium |
| F-05 | Verbose Error Handling / Sensitive Information Disclosure | 6.5 Medium |
| F-06 | Broken Function Level Authorization (BFLA) | 6.5 Medium |
| F-07 | Excessive Pagination / Unrestricted Resource Consumption | 4.3 Medium |
| F-08 | BOLA in Orders Search | 6.5 Medium |
| F-09 | Unsafe Consumption of Third-Party API Data | 5.3 Medium |
| F-10 | Missing Authentication Rate Limiting | 5.3 Medium |

The highest-priority finding demonstrated a complete **User → Admin privilege
escalation**. A regular user was able to modify the sensitive `Role` property,
authenticate again, receive effective administrative privileges and access an
administrative endpoint.

The assessment also identified a BOLA condition in an endpoint intended to
represent a secure implementation, demonstrating that authorization must be
validated consistently across every operation that exposes the same protected
resource.

### Negative Tests and Working Controls

The pentest also documented controls that worked correctly:

-   JWT role tampering without a valid signature was rejected.
-   Invalid or unsigned JWTs were rejected.
-   A normal user was denied access to `/api/v2/users`.
-   The secure individual-order endpoint prevented cross-user access.
-   The secure SSRF endpoint blocked local and private destinations.
-   The secure authentication endpoint applied rate limiting.
-   Duplicate coupon persistence was prevented by the database uniqueness constraint.

These negative tests help distinguish confirmed vulnerabilities from attack
paths that were tested but successfully blocked.

### Pentest Documentation

The public pentest documentation is maintained separately from the main README:

-   [Pentest Assessment](docs/pentest/Pentest_English.md)

The public documentation is sanitized and does not include generated JWTs,
password hashes, personal filesystem paths or other unnecessary sensitive
evidence.

------------------------------------------------------------------------

## 🗄️ Database

PostgreSQL is used to persist laboratory data.

Entity Framework Core handles:

-   entity mapping
-   database access
-   schema migrations
-   relationships
-   database constraints

Some security controls are intentionally enforced at the database layer
as well.

For example, the API6 laboratory uses a unique constraint to prevent
multiple coupon redemptions for the same user:

``` text
UNIQUE(UserId, CouponCode)
```

This demonstrates defense in depth between application validation and
database integrity.

------------------------------------------------------------------------

## 🔒 Security Techniques Demonstrated

The secure implementations demonstrate techniques including:

-   Object-level authorization
-   Property-level authorization
-   Role-based access control
-   JWT authentication
-   DTO-based input models
-   Server-side business-rule validation
-   Database uniqueness constraints
-   Resource limits
-   SSRF destination validation
-   Private and loopback address blocking
-   Secure error handling
-   Server-side exception logging
-   API lifecycle management
-   External API response validation
-   HTTP timeouts
-   Upstream failure handling

------------------------------------------------------------------------

## 📸 Evidence

Each laboratory contains screenshots demonstrating vulnerable and secure
behavior.

Evidence is stored under:

``` text
docs/images/
```

Examples include:

-   vulnerable HTTP responses
-   secure HTTP responses
-   authorization failures
-   PostgreSQL verification
-   server-side logs
-   simulated internal services
-   simulated third-party API responses

The detailed reports reference these images directly.

------------------------------------------------------------------------

## ⚠️ Security Warning

This repository intentionally contains vulnerable code.

It is designed for:

-   cybersecurity education
-   Application Security study
-   secure coding practice
-   API security testing
-   local laboratory exercises

It is **not** designed for:

-   production deployment
-   Internet-facing hosting
-   processing real user information
-   storing real credentials
-   handling sensitive information

Run the application only in an isolated and controlled environment.

------------------------------------------------------------------------

## 📚 References

-   [OWASP API Security
    Project](https://owasp.org/www-project-api-security/)
-   [OWASP API Security Top 10 ---
    2023](https://owasp.org/API-Security/editions/2023/en/0x11-t10/)
-   [OWASP Cheat Sheet Series](https://cheatsheetseries.owasp.org/)
-   [Microsoft ASP.NET Core
    Documentation](https://learn.microsoft.com/aspnet/core/)
-   [Entity Framework Core
    Documentation](https://learn.microsoft.com/ef/core/)

------------------------------------------------------------------------

## 👤 Author

**Alexandre Murayama**

Software Developer transitioning into Cybersecurity, with a focus on:

-   Application Security
-   API Security
-   Penetration Testing
-   Secure Software Development

------------------------------------------------------------------------

## 📄 License

This project is licensed under the **MIT License**.

See [LICENSE](LICENSE) for details.
