
$project = "LitExplore.Server"

Write-Host "Starting SQL Server"
docker start LitExploreDB

Write-Host "Running program"
dotnet run --project $project
