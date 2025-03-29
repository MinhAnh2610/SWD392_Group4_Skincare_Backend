[![Build Status](https://jenkins.pak160404.click/buildStatus/icon?job=SWD_SkinCare)](https://jenkins.pak160404.click/job/SWD_SkinCare/)
[![Lines of Code](https://sonarqube.pak160404.click/api/project_badges/measure?project=SWD_SkinCare&metric=ncloc&token=sqb_bd1957dc5e24f7d013f647a7b727f78bc7bae2c7)](https://sonarqube.pak160404.click/dashboard?id=SWD_SkinCare)
![Website](https://img.shields.io/website?url=https%3A%2F%2Fweb.pak160404.click%2F)
![Dynamic JSON Badge](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fapi.pak160404.click%2Fopenapi%2Fv1.json&query=%24.info.title&label=Api)


## Table of Contents
- [Project Overview](#project-overview)
- [System Architecture](#system-architecture)
  - [System Context](#system-context)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Setup Guide](#setup-guide)
  - [System Requirements](#system-requirements)
  - [Docker Setup](#docker-setup)
  - [Traditional Setup](#traditional-setup)
- [Database Management](#database-management)
  - [Using DBeaver](#using-dbeaver)
- [Troubleshooting](#troubleshooting)
- [API Documentation](#api-documentation)
- [External Integrations](#external-integrations)
- [Team](#team)
- [CI/CD](#cicd)

## Project Overview

The Skincare Backend is a .NET-based API that powers a skincare e-commerce platform. It includes comprehensive inventory management, order processing, user management, and product recommendation capabilities. The system follows a clean architecture approach with domain-driven design principles.

## System Architecture

The application follows a clean architecture pattern with these layers:

- **Domain Layer**: Core business entities and business rules
- **Application Layer**: Use cases, services, and interfaces
- **Infrastructure Layer**: Data persistence and external service integrations
- **Presentation Layer**: API controllers and endpoints

### System Context

```mermaid
graph TD
    subgraph External_Systems
        GHN[GHN Shipping API]
        VNPay[VNPay Payment Gateway]
        SMTP[Email Service]
        AzureBlob[Azure Blob Storage]
    end

    subgraph Actors
        Customer[Customer]
        Staff[Staff]
        Manager[Manager]
    end

    subgraph Frontend_Applications
        WebApp[Web Application]
        MobileApp[Mobile Application]
    end

    subgraph SkinCare_Backend
        API[API Endpoints/Controllers]
        Services[Business Services]
        Auth[Authentication/Authorization]
        Repositories[Data Access Layer]
        
        subgraph Domain
            Cosmetics[Cosmetics Management]
            Orders[Order Processing]
            Payments[Payment Processing]
            Inventory[Inventory Management]
            Reports[Reporting System]
            SkinQuiz[Skin Quiz System]
            EventCoupon[Events & Coupons]
            Users[User Management]
            Content[Content Management]
        end
    end

    subgraph Data_Storage
        Postgres[(PostgreSQL Database)]
        Redis[(Redis Cache)]
    end

    Customer --> WebApp
    Customer --> MobileApp
    Staff --> WebApp
    Manager --> WebApp
    WebApp --> API
    MobileApp --> API
    API --> Services
    API --> Auth
    Services --> Domain
    Services --> GHN
    Services --> VNPay
    Services --> SMTP
    Services --> AzureBlob
    Services --> Repositories
    Repositories --> Postgres
    Repositories --> Redis
```

## Features

### Authentication and User Management
- JWT-based authentication
- Role-based authorization (Customer, Staff, Manager)
- User profile management

### Product Catalog
- Product management with categories, brands, and types
- Image management with Azure Blob Storage
- Pricing with support for discounts and events

### Skin Quiz
- Interactive skin type assessment
- Product recommendations based on skin type
- Personalized skincare routines

### Order Processing
- Shopping cart functionality
- Multi-payment gateway integration (VNPay)
- Order status tracking
- Integration with GHN for shipping

### Inventory Management
- Batch tracking with expiration dates
- Stock management
- Low inventory alerts

### Sales and Reporting
- Revenue reports
- Product performance analytics
- Customer behavior analytics
- Export to multiple formats (PDF, Word)

### Promotions
- Event-based discounts
- Coupon management
- Promotional pricing

### Content Management
- Blog articles
- FAQs
- Product reviews and ratings

### Customer Support
- Chatwoot integration for live chat
- Support ticket management

## Technology Stack

- **Framework:** .NET 8.0
- **Database:** PostgreSQL 17
- **Caching:** Redis
- **ORM:** Entity Framework Core
- **Authentication:** JWT Tokens
- **Documentation:** Swagger/OpenAPI
- **Cloud Storage:** Azure Blob Storage
- **Payment Gateway:** VNPay
- **Shipping API:** GHN
- **Email Service:** SMTP Integration
- **Containerization:** Docker & Docker Compose

## Setup Guide

### System Requirements

- .NET 8.0 SDK
- PostgreSQL 17
- Redis (optional, for caching)
- Docker & Docker Compose (optional)

### Docker Setup

#### Prerequisites

- Docker installed
- Create a `.env` folder, then create `dbcon.env`:
  ```
  databaseConnectionString=Server=skincare.db;Database=skincare;User Id=admin;Password=secret;
  ```

#### Instructions

1. Disable any running PostgreSQL services on your machine.
2. Run `docker-compose up -d`
3. Access API at http://localhost:8080/swagger/index.html

### Traditional Setup

#### Prerequisites

- .NET 8.0 SDK installed
- PostgreSQL 17 installed and running

#### Instructions

1. Create a PostgreSQL database named `skincare`.
2. Update connection string in `appsettings.json`.
3. Apply database migrations:
   ```sh
   dotnet ef database update
   ```

## Database Management

### Using DBeaver

1. Click **New Database Connection** and select PostgreSQL.
2. Connect using:
   - Host: localhost
   - Database: skincare
   - Username: admin
   - Password: secret

## Troubleshooting

- **Database Connection Issues**: Ensure PostgreSQL is running and the connection string is correct.
- **Port Conflicts**: Modify ports in `docker-compose.yml` or use `--urls` parameter with `dotnet run`.
- **External Service Errors**: Check configuration values for GHN, VNPay, and Email.

## API Documentation

- Docker setup: [Swagger](http://localhost:8080/swagger/index.html)
- Traditional setup: [Swagger](https://localhost:7244/swagger/index.html)

## External Integrations

- **VNPay Payment Gateway**
- **GHN Shipping Provider**
- **Email Service (SMTP)**
- **Azure Blob Storage**

## Team

- **Lead Developer:** [Name]
- **Backend Developer:** [Name]
- **Database Engineer:** [Name]
- **DevOps Engineer:** [Name]

# CI/CD
https://jenkins.pak160404.click/job/SWD_SkinCare/

Backend for a Skincare System

# Guide to initialize Development Environment using Docker
## Prerequisites
- Docker is installed
- Create a .env folder, then create a dbcon.env at the root of your project solution ( on the same level of the docker compose)
##### dbcon.env
```
databaseConnectionString=Server=skincare.db;Database=skincare;User Id=admin;Password=secret;
```

## Instructions:
-  ### Step 0: 
 - Disable the current PostgreSQL service in services.msc.
- ### Step 1:
 - Run `docker-compose up`, this will create two services (Web API and Database).
- ### Step 2:
 - Web API should be accessible at http://localhost:8080/swagger/index.html
 
# Guide to update database
## Prerequisites
- Database container is running.
## Instructions:
- ### Step 1:
 - Open terminal and run:
 ```
dotnet ef database update --startup-project ./src/CleanArchitecture.Presentation/CleanArchitecture.Presentation.csproj --project ./src/CleanArchitecture.Infrastructure/CleanArchitecture.Infrastructure.csproj --connection "Server=localhost;Database=skincare;Port=5432;User Id=admin;Password=secret;"
```

# Guide to access Database on DBeaver
## Prerequisites
- Database container is running.
- DBeaver is installed.
## Instructions:
- ### Step 1: 
  - Click **`New Database Connection`** (or **`Ctrl + Shift + N`**) and choose PostgreSQL.
- ### Step 2: 
  - Connect by **Host** with the following settings:
   - Host: localhost
   - Database: skincare   
   - Authentication: Database Native
   - Username: admin
   - Password: secret
  
