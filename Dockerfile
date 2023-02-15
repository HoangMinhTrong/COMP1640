FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["COMP1640/COMP1640.csproj", "COMP1640/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Utilities/Utilities.csproj", "Utilities/"]

RUN dotnet restore "COMP1640/COMP1640.csproj"
COPY . .
WORKDIR "/src/COMP1640"
RUN dotnet build "COMP1640.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "COMP1640.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "COMP1640.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet COMP1640.dll