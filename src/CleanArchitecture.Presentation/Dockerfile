FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CleanArchitecture.Presentation/CleanArchitecture.Presentation.csproj", "src/CleanArchitecture.Presentation/"]
COPY ["src/CleanArchitecture.Application/CleanArchitecture.Application.csproj", "src/CleanArchitecture.Application/"]
COPY ["src/CleanArchitecture.Domain/CleanArchitecture.Domain.csproj", "src/CleanArchitecture.Domain/"]
COPY ["src/CleanArchitecture.Infrastructure/CleanArchitecture.Infrastructure.csproj", "src/CleanArchitecture.Infrastructure/"]
RUN dotnet restore "src/CleanArchitecture.Presentation/CleanArchitecture.Presentation.csproj"
COPY . .
WORKDIR "/src/src/CleanArchitecture.Presentation"
RUN dotnet build "CleanArchitecture.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CleanArchitecture.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchitecture.Presentation.dll"]
