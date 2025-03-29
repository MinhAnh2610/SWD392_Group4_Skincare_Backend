[![Build Status](https://jenkins.pak160404.click/buildStatus/icon?job=SWD_SkinCare)](https://jenkins.pak160404.click/job/SWD_SkinCare/)
[![Lines of Code](https://sonarqube.pak160404.click/api/project_badges/measure?project=SWD_SkinCare&metric=ncloc&token=sqb_bd1957dc5e24f7d013f647a7b727f78bc7bae2c7)](https://sonarqube.pak160404.click/dashboard?id=SWD_SkinCare)
![Website](https://img.shields.io/website?url=https%3A%2F%2Fweb.pak160404.click%2F)
![Dynamic JSON Badge](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fapi.pak160404.click%2Fopenapi%2Fv1.json&query=%24.info.title&label=Api)


# SWD392 Group 4 Skincare Backend

A comprehensive backend system for an e-commerce skincare platform with advanced features for product management, customer engagement, and sales processing.

## Table of Contents
- [SWD392 Group 4 Skincare Backend](#swd392-group-4-skincare-backend)
  - [Table of Contents](#table-of-contents)
  - [Project Overview](#project-overview)
  - [System Architecture](#system-architecture)
    - [System Context](#system-context)
- [Guide to access Database on DBeaver](#guide-to-access-database-on-dbeaver)
  - [Prerequisites](#prerequisites)
  - [Instructions:](#instructions)

## Project Overview

The Skincare Backend is a .NET-based API that powers a skincare e-commerce platform. It includes comprehensive inventory management, order processing, user management, and product recommendation capabilities. The system implements a clean architecture approach with separation of concerns and follows domain-driven design principles.

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

    %% Actor interactions
    Customer --> WebApp
    Customer --> MobileApp
    Staff --> WebApp
    Manager --> WebApp

    %% Frontend to Backend
    WebApp --> API
    MobileApp --> API

    %% API to Services
    API --> Services
    API --> Auth

    %% Services to Domain
    Services --> Domain

    %% Services to External Systems
    Services --> GHN
    Services --> VNPay
    Services --> SMTP
    Services --> AzureBlob

    %% Data access
    Services --> Repositories
    Repositories --> Postgres
    Repositories --> Redis

    %% External Systems responses
    GHN --> Services
    VNPay --> Services
    SMTP --> Services
    AzureBlob --> Services

    classDef external fill:#f9f,stroke:#333,stroke-width:2px
    classDef actor fill:#bbf,stroke:#333,stroke-width:2px
    classDef frontend fill:#bfb,stroke:#333,stroke-width:2px
    classDef storage fill:#fbb,stroke:#333,stroke-width:2px
    
    class External_Systems external
    class Actors actor
    class Frontend_Applications frontend
    class Data_Storage storage
Features
Authentication and User Management
JWT-based authentication
Role-based authorization (Customer, Staff, Manager)
User profile management
Product Catalog
Product management with categories, brands, and types
Image management with Azure Blob Storage
Pricing with support for discounts and events
Skin Quiz
Interactive skin type assessment
Product recommendations based on skin type
Personalized skincare routines
Order Processing
Shopping cart functionality
Multi-payment gateway integration (VNPay)
Order status tracking
Integration with GHN for shipping
Inventory Management
Batch tracking with expiration dates
Stock management
Low inventory alerts
Sales and Reporting
Revenue reports
Product performance analytics
Customer behavior analytics
Export to multiple formats (PDF, Word)
Promotions
Event-based discounts
Coupon management
Promotional pricing
Content Management
Blog articles
FAQs
Product reviews and ratings
Customer Support
Chatwoot integration for live chat
Support ticket management
Technology Stack
Framework: .NET 8.0
Database: PostgreSQL 17
Caching: Redis
ORM: Entity Framework Core
Authentication: JWT Tokens
Documentation: Swagger/OpenAPI
Cloud Storage: Azure Blob Storage
Payment Gateway: VNPay
Shipping API: GHN
Email Service: SMTP Integration
Containerization: Docker & Docker Compose
Setup Guide
System Requirements
.NET 8.0 SDK
PostgreSQL 17 (or use Docker container)
Redis (optional, for caching)
Docker & Docker Compose (optional, for containerized setup)
Option 1: Docker Setup (Recommended)
Prerequisites
Docker and Docker Compose installed
Create a .env folder, then create a dbcon.env file at the root of your project solution (on the same level as the docker-compose.yml)
databaseConnectionString=Server=skincare.db;Database=skincare;User Id=admin;Password=secret;
azureBlobConnectionString=YourAzureBlobConnectionString
Instructions:
Disable any running PostgreSQL services on your machine if they're using port 5432
Start the containers:
docker-compose up -d
Access the API:
API will be available at http://localhost:8080
Swagger documentation: http://localhost:8080/swagger/index.html
Option 2: Traditional Setup
Prerequisites
.NET 8.0 SDK installed
PostgreSQL 17 installed and running
Database Setup
Create a PostgreSQL database named skincare
Update connection string in src/CleanArchitecture.Presentation/appsettings.json:
"ConnectionStrings": {
  "DevDatabase": "Host=localhost;Port=5432;Database=skincare;User Id=your_username;Password=your_password;Include Error Detail=true;"
}
Apply Database Migrations
dotnet ef database update --startup-project ./src/CleanArchitecture.Presentation/CleanArchitecture.Presentation.csproj --project ./src/CleanArchitecture.Infrastructure/CleanArchitecture.Infrastructure.csproj
External Service Configuration
Email Service:
Update the following in appsettings.json:

"EmailConfiguration": {
  "From": "your-email@example.com",
  "SmtpServer": "smtp.example.com",
  "Port": 465,
  "Username": "your-email@example.com",
  "Password": "your-password"
}
GHN Shipping:
Update the following in appsettings.json:

"GHN": {
  "BaseUrl": "https://dev-online-gateway.ghn.vn/shiip/public-api",
  "Token": "your-ghn-token",
  "ShopId": "your-shop-id"
}
VNPay Payment:
Update the following in appsettings.json:

"Vnpay": {
  "TmnCode": "your-tmn-code",
  "HashSecret": "your-hash-secret",
  "BaseUrl": "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html",
  "ReturnUrl": "your-return-url"
}
Azure Blob Storage:
Set environment variable:

# Windows
setx azureBlobConnectionString "your-connection-string"

# macOS/Linux
export azureBlobConnectionString="your-connection-string"
Build and Run
Build the project:

dotnet build
Run the application:

dotnet run --project src/CleanArchitecture.Presentation
Access the API:

The API will be available at https://localhost:7244 and http://localhost:5223
Database Management
Using DBeaver to Access the Database
Click New Database Connection and choose PostgreSQL
Connect with the following settings:
Host: localhost
Port: 5432
Database: skincare
Authentication: Database Native
Username: admin (Docker) or your PostgreSQL username
Password: secret (Docker) or your PostgreSQL password
Troubleshooting
Database Connection Issues:

Ensure PostgreSQL is running on the expected port
Verify the connection string matches your PostgreSQL setup
For Docker setup, ensure the container is running
Port Conflicts:

If port 8080/8081 is already in use, modify the ports in docker-compose.yml
For traditional setup, you can specify a different port using --urls parameter with dotnet run
External Service Errors:

Ensure all configuration values for GHN, VNPay, and Email are correct
Check connectivity to these external services
API Documentation
Once the application is running, you can access the comprehensive API documentation using Swagger:

Docker setup: http://localhost:8080/swagger/index.html
Traditional setup: https://localhost:7244/swagger/index.html
The API includes these main resources:

/api/auth - Authentication endpoints
/api/cosmetics - Product catalog management
/api/orders - Order processing
/api/quiz - Skin quiz and recommendations
/api/reports - Analytics and reporting
/api/blogs - Content management
/api/users - User profile management
External Integrations
VNPay Payment Gateway
The system integrates with VNPay for secure online payments, supporting:

Credit/debit card processing
QR code payments
Payment status notifications
GHN Shipping Provider
Integration with Giao Hang Nhanh (GHN) shipping services:

Shipping rate calculation
Order tracking
Shipping status updates
Email Service
Automated email notifications for:

Order confirmations
Shipping updates
Password resets
Marketing campaigns
Azure Blob Storage
Used for storing and serving:

Product images
Blog content images
User profile pictures
Team
Lead Developer: [Name]
Backend Developer: [Name]
Database Engineer: [Name]
DevOps Engineer: [Name]
For questions or support, please contact: [your-email@example.com]


This README.md provides a comprehensive overview of your project, including setup instructions, architecture diagrams, feature descriptions, and external integrations. Feel free to customize sections like the Team members and contact information with your specific details.

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
  
