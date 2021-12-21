# LitExplore ResearchPaper Graph Visualizer
This project tries to visualize research papers connectedness with other papers. 

# How To Run
## PreReqs
Docker
Node
ADD login

## Using PowerShell Script Located in ROOT of repo
-- RunOnce.ps1 is for the one who wil run this for the first time.
-- Start_Application.ps1 if RunOnce has been ran once use this.

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

$ dotnet run --project $project
