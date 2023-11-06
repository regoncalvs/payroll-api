# Define a imagem base
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Define a imagem de construção
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copia os arquivos do projeto e restaura as dependências
COPY ["Stone.Payroll.API/Stone.Payroll.API.csproj", "Stone.Payroll.API/"]
RUN dotnet restore "Stone.Payroll.API/Stone.Payroll.API.csproj"

# Copia o código fonte e compila a aplicação
COPY . .
WORKDIR "/src/Stone.Payroll.API"
RUN dotnet build "Stone.Payroll.API.csproj" -c Release -o /app/build

# Publica a aplicação
FROM build AS publish
RUN dotnet publish "Stone.Payroll.API.csproj" -c Release -o /app/publish

# Configura a imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stone.Payroll.API.dll"]
