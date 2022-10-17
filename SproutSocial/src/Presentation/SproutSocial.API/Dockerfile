#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Presentation/SproutSocial.API/SproutSocial.API.csproj", "src/Presentation/SproutSocial.API/"]
COPY ["src/Infrastructure/SproutSocial.Persistence/SproutSocial.Persistence.csproj", "src/Infrastructure/SproutSocial.Persistence/"]
COPY ["src/Core/SproutSocial.Application/SproutSocial.Application.csproj", "src/Core/SproutSocial.Application/"]
COPY ["src/Core/SproutSocial.Domain/SproutSocial.Domain.csproj", "src/Core/SproutSocial.Domain/"]
COPY ["src/Infrastructure/SproutSocial.Infrastructure/SproutSocial.Infrastructure.csproj", "src/Infrastructure/SproutSocial.Infrastructure/"]
RUN dotnet restore "src/Presentation/SproutSocial.API/SproutSocial.API.csproj"
COPY . .
WORKDIR "/src/src/Presentation/SproutSocial.API"
RUN dotnet build "SproutSocial.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SproutSocial.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SproutSocial.API.dll"]
