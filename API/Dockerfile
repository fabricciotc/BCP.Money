#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["API/API.csproj", "API/"]
COPY ["Aplicacion/Aplicacion.csproj", "Aplicacion/"]
COPY ["Persistencia/Persistencia.csproj", "Persistencia/"]
COPY ["Dominio/Dominio.csproj", "Dominio/"]
COPY ["Seguridad/Seguridad.csproj", "Seguridad/"]
RUN dotnet restore "API/API.csproj"
COPY . .
WORKDIR "/src/API"
RUN dotnet build "API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "API.dll"]