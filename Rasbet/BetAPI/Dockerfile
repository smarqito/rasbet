#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BetAPI/BetAPI.csproj", "BetAPI/"]
RUN dotnet restore "BetAPI/BetAPI.csproj"
COPY . .
WORKDIR "/src/BetAPI"
RUN dotnet build "BetAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BetAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BetAPI.dll"]