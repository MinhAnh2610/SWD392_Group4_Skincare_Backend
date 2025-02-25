[![Build Status](https://jenkins.pak160404.click/buildStatus/icon?job=SWD_SkinCare)](https://jenkins.pak160404.click/job/SWD_SkinCare/)
[![Lines of Code](https://sonarqube.pak160404.click/api/project_badges/measure?project=SWD_SkinCare&metric=ncloc&token=sqb_bd1957dc5e24f7d013f647a7b727f78bc7bae2c7)](https://sonarqube.pak160404.click/dashboard?id=SWD_SkinCare)
![Website](https://img.shields.io/website?url=https%3A%2F%2Fweb.pak160404.click%2F)
![Dynamic JSON Badge](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fapi.pak160404.click%2Fopenapi%2Fv1.json&query=%24.info.title&label=Api)


# Skincare System
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
  
