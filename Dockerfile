# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos de projeto
COPY FastTech.Pedidos.Domain/ ./FastTech.Pedidos.Domain/
COPY FastTech.Pedidos.Application/ ./FastTech.Pedidos.Application/
COPY FastTech.Pedidos.Infra/ ./FastTech.Pedidos.Infra/
COPY FastTech.Pedidos/ ./FastTech.Pedidos/

# Restaura os pacotes
RUN dotnet restore "FastTech.Pedidos/FastTech.Pedidos.csproj"

# Publica o projeto
RUN dotnet publish "FastTech.Pedidos/FastTech.Pedidos.csproj" -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "FastTech.Pedidos.dll"]
