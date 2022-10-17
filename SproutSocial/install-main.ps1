# stop contianer

docker stop sproutsocial.api
docker stop mssql

# remove container

docker rm sproutsocial.api
docker rm mssql

# remove images

docker rmi $(docker images 'mssql' -a -q)
docker rmi $(docker images 'sproutsocialapi' -a -q)

# remove all networks
docker network prune -af

# remove all volumes
docker volume rm $(docker volume ls -q)

#docker system prune -af

docker-compose --env-file ./config/.env up -d --build

# migrate database
#dotnet ef database update --project .\src\Infrastructure\SproutSocial.Persistence --startup-project .\src\Presentation\SproutSocial.API