FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FastTechFoods.Orders.Web/FastTechFoods.Orders.Web.csproj", "FastTechFoods.Orders.Web/"]
RUN dotnet restore "FastTechFoods.Orders.Web/FastTechFoods.Orders.csproj"
COPY . .
WORKDIR "/src/FastTechFoods.Orders.Web"
RUN dotnet build "FastTechFoods.Orders.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FastTechFoods.Orders.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FastTechFoods.Orders.Web.dll"]
