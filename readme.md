 # CORE 7 WebApi

## EF dotnet cmd
-------------------------------------------------------

```

//initial 
dotnet ef migrations add "Initial" --project C:\files\git\pets\CoreSB_7\CoreSBBL\CoreSBBL.csproj --startup-project C:\files\git\pets\CoreSB_7\CoreSBServer\CoreSBServer.csproj --verbose

//update
dotnet ef database update --project C:\files\git\pets\CoreSB_7\CoreSBBL\CoreSBBL.csproj --startup-project C:\files\git\pets\CoreSB_7\CoreSBServer\CoreSBServer.csproj --verbose

//gen script
dotnet ef migrations script --project C:\files\git\pets\CoreSB_7\CoreSBBL\CoreSBBL.csproj --startup-project C:\files\git\pets\CoreSB_7\CoreSBServer\CoreSBServer.csproj --verbose

```

## Docker commands
-------------------------------------------------------

docker pull mcr.microsoft.com/mssql/server

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=QwErTy_1" -e "MSSQL_PID=Express" -p 1433:1433 --name msSQL -d
mcr.microsoft.com/mssql/server:latest

-------------------------------------------------------
