


# How To Run
## Initial setup.
### Env Vars
Make sure docker is installed and setup.
Then run:
// To setup DB Only run once 
$ $ProjectDB = "LitExplore.Entity"
$ $project = "LitExplore.Server"
$ $database = "LitExplore"
$ $password = "2cb057ee-5078-4795-80ed-23b9c5af2e03" #New-Guid
$ $connectionString = "Server=localhost;Database=$database;User Id=sa;Password=$password;

### Create the WebTemplate from source.
// Under assumption that Node.js has been installed on the system. 
run the following:

$ cd .\LitExplore.UI\ | npm install | npm run buildcss | cd..

### Run Of DB and AppServer
// For Multiple uses, use this Line instead: 
$ docker start LitExploreDB

// After This have been run atleast once. I.e. If this is the first time running it.
$ docker run --name "LitExploreDB" -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
Trusted_Connection=True;Encrypt=False"

$ dotnet run --project $project



# There are Lies Below..
# Lit Explore - Analysis Design and Software Architecture
The program is run through a Linux Docker container.
## How to run program:
$ docker-compose up

## How to run unit tests:
$ docker-compose up --rm tests


## Overleaf edit link: 
https://www.overleaf.com/1463845817gbvjhrggqmsy

## Vertical Slice requirements
### Front end (e.g. Blazor or Xamarin.Forms)
### API (ASP.NET Core Web Api)
### Services/repository
### Infrastructure (e.g. SQL Database or Azure Cosmos DB)
### Authentication/authorization

