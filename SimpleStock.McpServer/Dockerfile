FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 3001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SimpleStock.McpServer/SimpleStock.McpServer.csproj", "SimpleStock.McpServer/"]
RUN dotnet restore "SimpleStock.McpServer/SimpleStock.McpServer.csproj"
COPY . .
WORKDIR "/src/SimpleStock.McpServer"
RUN dotnet build "SimpleStock.McpServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleStock.McpServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleStock.McpServer.dll"] 