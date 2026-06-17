# BUILD
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copiar archivos .csproj con sus nombres REALES
COPY ["Backend_PlanoriaCapstone/Backend_PlanoriaCapstone.csproj", "Backend_PlanoriaCapstone/"]
COPY ["PlanoriaCapstone.DLL/PlanoriaCapstone.Bll.csproj", "PlanoriaCapstone.DLL/"]
COPY ["PlanoriaCapstone.Dal/PlanoriaCapstone.Dal.csproj", "PlanoriaCapstone.Dal/"]
COPY ["PlanoriaCapstone.DTOs/PlanoriaCapstone.DTOs.csproj", "PlanoriaCapstone.DTOs/"]

# Restaurar dependencias
RUN dotnet restore "Backend_PlanoriaCapstone/Backend_PlanoriaCapstone.csproj"

# Copiar el resto del código
COPY . .

# Publicar
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