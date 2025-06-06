FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FastTech.Pedidos/FastTech.Pedidos.csproj", "FastTech.Pedidos/"]
RUN dotnet restore "FastTech.Pedidos/FastTech.Pedidos.csproj"
COPY . .
WORKDIR "/src/FastTech.Pedidos"
RUN dotnet build "FastTech.Pedidos.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FastTech.Pedidos.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FastTech.Pedidos.dll"]
