#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
#EXPOSE 5000
#EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Cornplex.Api/Cornplex.Api.csproj", "src/Cornplex.Api/"]
COPY ["src/Cornplex.Infrastructure/Cornplex.Infrastructure.csproj", "src/Cornplex.Infrastructure/"]
COPY ["src/Cornplex.Service/Cornplex.Service.csproj", "src/Cornplex.Service/"]
COPY ["src/Cornplex.Persistence/Cornplex.Persistence.csproj", "src/Cornplex.Persistence/"]
COPY ["src/Cornplex.Domain/Cornplex.Domain.csproj", "src/Cornplex.Domain/"]
RUN dotnet restore "src/Cornplex.Api/Cornplex.Api.csproj"
COPY . .
WORKDIR "/src/src/Cornplex.Api"
RUN dotnet build "Cornplex.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cornplex.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cornplex.Api.dll"]
