# core 7 cloud native MSA template
# SOLID, Vertical slice + DDD + layered/onion/clean
# KISS DRY

## Packages to build core:
-------------------------------------------------------

```
dotnet add package Autofac 
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Autofac.Extensions.DependencyInjection

dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer 
dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore

dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL 

dotnet add package MongoDB.Bson 
dotnet add package MongoDB.Driver 
dotnet add package MongoDB.Driver.Core 

dotnet add package NEST 

dotnet add package Elastic.Clients.Elasticsearch
dotnet add package Elasticsearch.Net 

```


## Docker CLI infrastructure init
-------------------------------------------------------

```

#mssql
	docker pull mcr.microsoft.com/mssql/server

	docker run --name msSQL -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=coresbQ1" -e "MSSQL_PID=Express" -p 1433:1433 -d mcr.microsoft.com/mssql/server:latest 

#Mongo
	docker pull mongo

	docker run --name localMongo -e MONGO_INITDB_ROOT_USERNAME=admin -e MONGO_INITDB_ROOT_PASSWORD=mongoadmin -p 27017:27017 mongo

#elastic
	docker pull docker.elastic.co/elasticsearch/elasticsearch:8.2.2 
	docker tag docker.elastic.co/elasticsearch/elasticsearch:8.2.2 elasticsearch
	docker network create elastic
	
	docker run --name elastic --net elastic -p 9200:9200 -p 9300:9300 -it elasticsearch

#kibana
	docker pull docker.elastic.co/kibana/kibana:8.2.2
	docker tag docker.elastic.co/kibana/kibana:8.2.2 kibana

	docker run --name kibana --net elastic -p 5601:5601 docker.elastic.co/kibana/kibana:8.2.2

```