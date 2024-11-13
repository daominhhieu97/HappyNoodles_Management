FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["HappyNoodles_ManagementApp.csproj", "."]
RUN dotnet restore "./HappyNoodles_ManagementApp.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "HappyNoodles_ManagementApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HappyNoodles_ManagementApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HappyNoodles_ManagementApp.dll"]

EXPOSE 8080