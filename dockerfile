# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY GestaoDeResiduos/*.csproj ./GestaoDeResiduos/
RUN dotnet restore ./GestaoDeResiduos/GestaoDeResiduos.csproj

# Copy the rest of the files and build the application
COPY . ./
RUN dotnet publish GestaoDeResiduos/GestaoDeResiduos.csproj -c Release -o out

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "GestaoDeResiduos.dll"]
