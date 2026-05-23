# BUILD
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore "Backend_PlanoriaCapstone/Backend_PlanoriaCapstone.csproj"

RUN dotnet publish "Backend_PlanoriaCapstone/Backend_PlanoriaCapstone.csproj" \
    -c Release \
    -o /app/publish

# RUNTIME
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Backend_PlanoriaCapstone.dll"]