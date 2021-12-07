# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

COPY . /source
WORKDIR /source/LitExplore

RUN dotnet restore

RUN dotnet publish --configuration Release --output /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5077/tcp
ENV ASPNETCORE_URLS http://*:5077

EXPOSE 7207/tcp
ENV ASPNETCORE_URLS https://*:7207

ENTRYPOINT ["dotnet", "LitExplore.dll"]