FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY Comex.API/ComexAPI.csproj ./

RUN dotnet restore Comex.API/ComexAPI.csproj

COPY . ./

RUN dotnet build Comex.API/ComexAPI.csproj -c Release -o /app/build

RUN dotnet publish Comex.API/ComexAPI.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "ComexAPI.dll"]