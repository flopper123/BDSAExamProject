# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

ENV Logging__Console__FormatterName=simple

COPY . /source


WORKDIR /source


WORKDIR /source/LitExplore.Server

RUN dotnet restore
RUN dotnet publish --configuration Release --output /app


FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY --from=build /app ./

EXPOSE 5141/tcp
ENV ASPNETCORE_URLS http://*:5141

EXPOSE 7252/tcp
ENV ASPNETCORE_URLS https://*:7252

ENTRYPOINT ["dotnet", "LitExplore.Server.dll"]