FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app 

COPY *.sln .
COPY Products.Api/*.csproj ./Products.Api/
COPY Products.Domain/*.csproj ./Products.Domain/
COPY Products.Infra/*.csproj ./Products.Infra/
COPY Products.Tests/*.csproj ./Products.Tests/

RUN dotnet restore 

COPY Products.Api/. ./Products.Api/
COPY Products.Domain/. ./Products.Domain/
COPY Products.Infra/. ./Products.Infra/ 
COPY Products.Tests/. ./Products.Tests/ 

WORKDIR /app/Products.Api
RUN dotnet publish -c Release -o out 

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 

COPY --from=build /app/Products.Api/out ./
ENTRYPOINT ["dotnet", "Products.Api.dll"]