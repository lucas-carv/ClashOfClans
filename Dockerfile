# Etapa de runtime (onde a API vai rodar)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia todo o código da solução para dentro da imagem
COPY . .

# Restaura os pacotes da API (ajuste o caminho se o nome da pasta/projeto mudar)
RUN dotnet restore "ClashOfClans.API/ClashOfClans.API.csproj"

# Publica a API
RUN dotnet publish "ClashOfClans.API/ClashOfClans.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# DLL que você confirmou que é esse nome 👇
ENTRYPOINT ["dotnet", "ClashOfClans.API.dll"]
