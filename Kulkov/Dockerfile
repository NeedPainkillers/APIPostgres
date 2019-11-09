FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Kulkov/Kulkov.csproj", "Kulkov/"]
RUN dotnet restore "Kulkov/Kulkov.csproj"
COPY . .
WORKDIR "/src/Kulkov"
RUN dotnet build "Kulkov.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kulkov.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kulkov.dll"]