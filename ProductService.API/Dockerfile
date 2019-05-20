FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProductService.API/ProductService.API.csproj", "ProductService.API/"]
COPY ["ProductService.Domain/ProductService.Domain.csproj", "ProductService.Domain/"]
COPY ["ProductService.Data/ProductService.Data.csproj", "ProductService.Data/"]
COPY ["ProductService.Model/ProductService.Model.csproj", "ProductService.Model/"]
RUN dotnet restore "ProductService.API/ProductService.API.csproj"
COPY . .
WORKDIR "/src/ProductService.API"
RUN dotnet build "ProductService.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProductService.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProductService.API.dll"]