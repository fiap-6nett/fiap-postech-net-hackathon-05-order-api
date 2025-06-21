# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 7100

# Copia os arquivos de projeto
COPY FastTechFoods.Orders.Domain/ ./FastTechFoods.Orders.Domain/
COPY FastTechFoods.Orders.Application/ ./FastTechFoods.Orders.Application/
COPY FastTechFoods.Orders.Infra/ ./FastTechFoods.Orders.Infra/
COPY FastTechFoods.Orders.Web/ ./FastTechFoods.Orders.Web/

# Restaura os pacotes
RUN dotnet restore "FastTechFoods.Orders.Web/FastTechFoods.Orders.Web.csproj"

# Publica o projeto
RUN dotnet publish "FastTechFoods.Orders.Web/FastTechFoods.Orders.Web.csproj" -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "FastTechFoods.Orders.Web.dll"]
