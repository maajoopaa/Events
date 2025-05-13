FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Events.API/Events.API.csproj", "Events.API/"]
COPY ["Events.Application/Events.Application.csproj", "Events.Application/"]
COPY ["Events.DataAccess/Events.DataAccess.csproj", "Events.DataAccess/"]
COPY ["Events.Domain/Events.Domain.csproj", "Events.Domain/"]
RUN dotnet restore "./Events.API/Events.API.csproj"
COPY . .
WORKDIR "/src/Events.API"
RUN dotnet build "./Events.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Events.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Events.API.dll"]