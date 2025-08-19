# Estágio 1: Construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Copia o arquivo .csproj e restaura as dependências
COPY gerenciamento.csproj ./
RUN dotnet restore gerenciamento.csproj

# Copia o resto da aplicação e publica
COPY . ./
RUN dotnet publish -c Release -o out

# Estágio 2: Rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build-env /app/out .

# Configuração da porta
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "gerenciamento.dll"]