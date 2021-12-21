
$project = "LitExplore.Server"

Write-Host "Starting SQL Server"
doker start LitExploreDB

Write-Host "Running program"
dotnet run --project $project
