
$project = "LitExplore.Server"

$password = "a3060c73-b054-40b2-bc24-544bceb20e95" #New-Guid

Write-Host "Starting SQL Server"
docker run --name "LitExploreDE" -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
$database = "LitExplore"
$connectionString = "Server=localhost;Database=$database;User Id=sa;Password=$password;Trusted_Connection=False;Encrypt=False"

Write-Host "Running program"
dotnet run --project $project

# RUN FIRST TMIE YOU CONNECT
# Write-Host "Configuring Connection String"
# dotnet user-secrets init --project $project
# dotnet user-secrets set "ConnectionStrings:LitExplore" "$connectionString" --project $project  | 
#