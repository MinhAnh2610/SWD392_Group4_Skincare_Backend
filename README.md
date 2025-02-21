# Skincare System

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
  
