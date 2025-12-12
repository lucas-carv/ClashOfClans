# Etapa de runtime (onde a API vai rodar)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copia o código TODO da solução
COPY . .

# ⬇️ MUITO IMPORTANTE: entra na pasta do PROJETO DA API
WORKDIR /ClashOfClans.API

# restaura os pacotes da API (e dos projetos referenciados)
RUN dotnet restore

# publica a API em Release
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# imagem final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# se o nome do .csproj for outro, troque aqui também
ENTRYPOINT ["dotnet", "ClashOfClans.API.dll"]
