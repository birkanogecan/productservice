FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProductService.MongoConsumer/ProductService.MongoConsumer.csproj", "ProductService.MongoConsumer/"]
COPY ["ProductService.Data/ProductService.Data.csproj", "ProductService.Data/"]
COPY ["ProductService.Model/ProductService.Model.csproj", "ProductService.Model/"]
RUN dotnet restore "ProductService.MongoConsumer/ProductService.MongoConsumer.csproj"
COPY . .
WORKDIR "/src/ProductService.MongoConsumer"
RUN dotnet build "ProductService.MongoConsumer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProductService.MongoConsumer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProductService.MongoConsumer.dll"]