# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

COPY . /source
WORKDIR /source/LitExplore
RUN dotnet restore

RUN dotnet publish --configuration Release --output /app1

#if the node is set between WORKDIR and run dotnet restore
#it will complain that it can't find "app1" when copying
FROM node:16.13.1
COPY ["package.json", "package-lock.json", "./"]
RUN npm install
COPY . .
CMD [ -d "node_modules" ] && npm run start || npm ci && npm run start
#the outcommented above does not help solving the problem

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app1
COPY --from=build /app1 ./


EXPOSE 5141/tcp
ENV ASPNETCORE_URLS http://*:5141

EXPOSE 7252/tcp
ENV ASPNETCORE_URLS https://*:7252

ENTRYPOINT ["dotnet", "LitExplore.dll"]