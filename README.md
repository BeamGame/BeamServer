# BeamServer

Create a beam database on your local postgreSQL

Update DefaultConnection in the appsettings.json to match your local database or add it on appsettings.Development.json

## Add migration 
```
cd BeamServer
dotnet ef migrations add MigrationName
dotnet ef database update
```