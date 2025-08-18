# Estágio 1: Construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia o arquivo .csproj e restaura as dependências
# (Agora este comando funcionará!)
COPY gerenciamento.csproj ./
RUN dotnet restore gerenciamento.csproj

# Copia o resto da aplicação e publica
COPY . ./
RUN dotnet publish -c Release -o out

# Estágio 2: Rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Configuração da porta
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "gerenciamento.dll"]