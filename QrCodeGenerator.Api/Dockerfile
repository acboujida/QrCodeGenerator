FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY QrCodeGenerator.Api.csproj ./

RUN dotnet restore QrCodeGenerator.Api.csproj

COPY . .

RUN dotnet publish QrCodeGenerator.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

ENV ASPNETCORE_HTTP_PORTS=8080

COPY --from=build /app/publish .

USER $APP_UID

ENTRYPOINT ["dotnet", "QrCodeGenerator.Api.dll"]