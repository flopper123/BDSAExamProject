
Write-Host "Setting up Env Variables" -ForegroundColor DarkCyan

$ProjectDB = "LitExplore.Entity"
$project = "LitExplore.Server"
$database = "LitExplore"
$password = "2cb057ee-5078-4795-80ed-23b9c5af2e03" #New-Guid
$connectionString = "Server=localhost;Database=$database;User Id=sa;Password=$password;"
dotnet user-secrets set "ConnectionStrings:LitExplore" "$connectionString" --project $projectDB
Write-Host "DONE" -ForegroundColor DarkGreen

Write-Host "Checking if Node is Installed." -ForegroundColor DarkCyan
node -v
npm -v

Write-Host "Build WebPage" -ForegroundColor DarkCyan

cd .\LitExplore.UI\ | npm install | npm run buildcss | cd..

Write-Host "Built" -ForegroundColor DarkGreen

Write-Host "Creating And Starting Docker DB Container " -ForegroundColor DarkCyan

docker run --name "LitExploreDB" -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest

Write-Host "Created and Started " -ForegroundColor DarkGreen

Write-Host ""For Future use after this, Use: docker start LitExploreDB" -ForegroundColor White

Write-Host "Starting WebServer"." -ForegroundColor DarkCyan
dotnet run --project $project