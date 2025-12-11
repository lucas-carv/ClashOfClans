# Imagem base só para rodar a API
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Porta interna que a API vai escutar
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Imagem para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia tudo pro container
COPY . .

# Restaura e publica em Release
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# TROCAR NomeDaSuaApi.dll pelo nome correto do seu projeto
ENTRYPOINT ["dotnet", "NomeDaSuaApi.dll"]
