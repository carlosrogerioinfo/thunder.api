# SDK do projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar arquivos e resraura dependencias
COPY ["src/services/Thunder.Project.Api/Thunder.Project.Api.csproj", "/app/src/services/Thunder.Project.Api/"]
COPY ["src/building_blocks/Thunder.Project.Core/Thunder.Project.Core.csproj", "/app/src/building_blocks/Thunder.Project.Core/"]
COPY ["src/building_blocks/Thunder.Project.Domain/Thunder.Project.Domain.csproj", "/app/src/building_blocks/Thunder.Project.Domain/"]
COPY ["src/building_blocks/Thunder.Project.Infrastructure/Thunder.Project.Infrastructure.csproj", "/app/src/building_blocks/Thunder.Project.Infrastructure/"]
COPY ["src/building_blocks/Thunder.Project.Service/Thunder.Project.Service.csproj", "/app/src/building_blocks/Thunder.Project.Service/"]

RUN dotnet restore "src/services/Thunder.Project.Api/Thunder.Project.Api.csproj"
RUN dotnet restore "src/building_blocks/Thunder.Project.Core/Thunder.Project.Core.csproj"
RUN dotnet restore "src/building_blocks/Thunder.Project.Domain/Thunder.Project.Domain.csproj"
RUN dotnet restore "src/building_blocks/Thunder.Project.Infrastructure/Thunder.Project.Infrastructure.csproj"
RUN dotnet restore "src/building_blocks/Thunder.Project.Service/Thunder.Project.Service.csproj"

# Gerar e confiar no certificado SSL (somente em desenvolvimento)
RUN dotnet dev-certs https --trust

# Copia tudo pra imagem
COPY . .

# Compilação do projeto
RUN dotnet build "src/services/Thunder.Project.Api/Thunder.Project.Api.csproj" -c Release -o /app/build

# Publicação do projeto
FROM build AS publish
RUN dotnet publish "src/services/Thunder.Project.Api/Thunder.Project.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagem de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar o conteúdo publicado pasta de execução
COPY --from=publish /app/publish .

# Expor a porta
EXPOSE 8080

# Define ponto de entrada
ENTRYPOINT ["dotnet", "Thunder.Project.Api.dll"]