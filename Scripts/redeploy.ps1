docker build -t hands-on-api .
docker stop hands-on-dev-container
docker rm hands-on-dev-container
docker run -d -p 8080:8080 --network container_network --name hands-on-dev-container hands-on-dev-api