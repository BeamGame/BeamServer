# BeamServer

Create a beam database on your local postgreSQL

Update DefaultConnection in the appsettings.json to match your local database or add it on appsettings.Development.json

## Add migration 
```
cd BeamServer
dotnet ef migrations add MigrationName
dotnet ef database update
```

## How it works

The server use the beam sdk to mint/swap ...

The minter role is dedicated of yser with the name Minter in the database

We use internal DB in posgresSql to save player et beamon datas

The nft metadata is linked to database table monsters
