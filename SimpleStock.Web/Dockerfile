FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SimpleStock.Web/SimpleStock.Web.csproj", "SimpleStock.Web/"]
RUN dotnet restore "SimpleStock.Web/SimpleStock.Web.csproj"
COPY . .
WORKDIR "/src/SimpleStock.Web"
RUN dotnet build "SimpleStock.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleStock.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleStock.Web.dll"] 