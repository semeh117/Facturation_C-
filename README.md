# FacturationTn

> Tunisian tax-compliant invoicing system, built with **.NET 10** using **Clean Architecture** and a modern **Blazor Server** interface.

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![EF Core](https://img.shields.io/badge/EF%20Core-10-512BD4)
![Blazor](https://img.shields.io/badge/UI-Blazor%20Server-512BD4)
![SQLite](https://img.shields.io/badge/DB-SQLite-003B57)
![Radzen](https://img.shields.io/badge/Components-Radzen-FF6600)

---

## Table of Contents

- [Overview](#overview)
- [Authentication and Roles](#authentication-and-roles)
- [Tunisian Specifics](#tunisian-specifics)
- [Architecture](#architecture)
- [Technical Stack](#technical-stack)
- [Quick Start](#quick-start)
- [Project Structure](#project-structure)
- [EF Core Migrations](#ef-core-migrations)

---

## Overview

**FacturationTn** is a web-based invoicing management application designed for Tunisian SMEs and freelancers. It allows for managing clients and products, and issuing invoices that comply with local tax standards (VAT, stamp duty, tax identification numbers).

The interface is built with **Blazor Server** and enhanced by **Radzen** components for interactive dashboards and smooth data management.

## Authentication and Roles

The application uses **ASP.NET Core Identity** to secure data access.

### Default Administrator Account
Upon startup, the application automatically creates an administrator account if it doesn't exist:
- **Email:** `semeh@gmail.com`
- **Password:** `semeh123`
- **Role:** `Admin`

### Security
- Management pages (Products, Clients, Invoices, Dashboard) are protected by the `[Authorize(Roles = "Admin")]` attribute.
- Automatic redirection to the login page is in place for unauthenticated users.

## Tunisian Specifics

| Element | Implementation |
|---|---|
| **Stamp Duty** | Fixed amount of `1.000 TND` added to each total invoice |
| **Multi-rate VAT** | Variable rates per product: `7 %`, `13 %`, `19 %` |
| **Tax ID (Matricule Fiscal)** | Dedicated field on the client (CIN or company ID) |
| **Monetary Precision** | `decimal(18, 3)` — milliemes of a dinar (TND) |
| **Price Snapshot** | Net price and VAT rate are frozen on the invoice at the time of creation. |

## Architecture

The application follows **Clean Architecture** principles:
- **FacturationTn.Domain**: Core business logic (Entities, Enums).
- **FacturationTn.Application**: Business logic (Services).
- **FacturationTn.Infrastructure**: Persistence (EF Core, SQLite, Identity).
- **FacturationTn.Web**: User Interface (Blazor Components, Radzen).

## Technical Stack

- **Framework:** .NET 10
- **UI:** Blazor Server + Radzen Blazor Components
- **ORM:** Entity Framework Core 10
- **Database:** SQLite
- **Identity:** ASP.NET Core Identity

## Quick Start

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Installation and Launch
```bash
# 1. Clone the repository
git clone <repo-url>
cd FacturationTn

# 2. Restore dependencies
dotnet restore

# 3. Apply migrations (creates DB and Identity tables)
dotnet ef database update --project FacturationTn.Infrastructure --startup-project FacturationTn.Web

# 4. Run the application
dotnet run --project FacturationTn.Web
```

Access `https://localhost:5001` (or the displayed port) and log in with `semeh@gmail.com` / `semeh123`.

## Project Structure

```
FacturationTn/
├── FacturationTn.Domain/        # Entities (Client, Product, Invoice...)
├── FacturationTn.Application/   # Calculation services
├── FacturationTn.Infrastructure/# DbContext, Identity, Migrations
└── FacturationTn.Web/           # Blazor components (.razor), Program.cs
```

## EF Core Migrations

To add a migration:
```bash
dotnet ef migrations add MigrationName --project FacturationTn.Infrastructure --startup-project FacturationTn.Web
```

---
*FacturationTn Project © 2026*
