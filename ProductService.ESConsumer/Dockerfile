FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProductService.ESConsumer/ProductService.ESConsumer.csproj", "ProductService.ESConsumer/"]
COPY ["ProductService.Data/ProductService.Data.csproj", "ProductService.Data/"]
COPY ["ProductService.Model/ProductService.Model.csproj", "ProductService.Model/"]
RUN dotnet restore "ProductService.ESConsumer/ProductService.ESConsumer.csproj"
COPY . .
WORKDIR "/src/ProductService.ESConsumer"
RUN dotnet build "ProductService.ESConsumer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProductService.ESConsumer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProductService.ESConsumer.dll"]