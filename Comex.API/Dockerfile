FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ComexAPI.csproj ./

RUN dotnet restore

COPY . ./

RUN dotnet build ComexAPI.csproj -c Release -o /app/build

RUN dotnet publish ComexAPI.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "ComexAPI.dll"]