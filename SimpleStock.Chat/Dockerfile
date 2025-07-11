FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SimpleStock.Chat/SimpleStock.Chat.csproj", "SimpleStock.Chat/"]
RUN dotnet restore "SimpleStock.Chat/SimpleStock.Chat.csproj"
COPY . .
WORKDIR "/src/SimpleStock.Chat"
RUN dotnet build "SimpleStock.Chat.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleStock.Chat.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleStock.Chat.dll"] 