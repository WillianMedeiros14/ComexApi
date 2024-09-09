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

RUN dotnet tool install --global dotnet-ef

ENV PATH="${PATH}:/root/.dotnet/tools"

ENTRYPOINT ["sh", "-c", "dotnet ef database update && dotnet ComexAPI.dll"]
