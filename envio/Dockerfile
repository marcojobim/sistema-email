# Estágio 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia o arquivo de solução e restaura as dependências
COPY EmailSender.sln .
COPY EmailSender/EmailSender.csproj EmailSender/
RUN dotnet restore

# Copia o resto da aplicação e publica
COPY . .
WORKDIR /app/EmailSender
RUN dotnet publish -c Release -o out

# Estágio 2: Rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/EmailSender/out .

# Configuração da porta
ENV ASPNETCORE_URLS=http://+:3000
EXPOSE 3000

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "EmailSender.dll"]