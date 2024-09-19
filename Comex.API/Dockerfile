FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app


COPY Comex.API/ComexAPI.csproj Comex.API/

RUN dotnet restore Comex.API/ComexAPI.csproj

COPY . .

WORKDIR /app/Comex.API

RUN dotnet build ComexAPI.csproj -c Release -o /app/build

RUN dotnet publish ComexAPI.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "ComexAPI.dll"]
