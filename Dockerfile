# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

ENV Logging__Console__FormatterName=simple

COPY . /source
WORKDIR /source/LitExplore

RUN dotnet restore
RUN dotnet publish --configuration Release --output /app

#if the node is set between WORKDIR and run dotnet restore
#it will complain that it can't find "app1" when copying
FROM node:16.13.1
WORKDIR /app

RUN npm install
RUN npm run buildcss 

#the outcommented above does not help solving the problem

#RUN dotnet publish --configuration Release --output /app1
#RUN dotnet run --project .

FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY --from=build /app ./

EXPOSE 5141/tcp
ENV ASPNETCORE_URLS http://*:5141

EXPOSE 7252/tcp
ENV ASPNETCORE_URLS https://*:7252

ENTRYPOINT ["dotnet", "LitExplore.dll"]