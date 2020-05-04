# Engie.GEM
- This is my solution for the coding test of Engie (https://github.com/gem-spaas/powerplant-coding-challenge).

# Solution structure
There are 3 projects:
  - Engie.GEM.ProductionPlan.API
    - This is the main API project. Test using endpoint POST localhost:5000/PowerProduction
  - Engie.GEM.ProductionPlan.Core
    - Contains the core business logics and domain entities.
  - Engie.GEM.ProductionPlan.Core.Test
    - Unit tests for the core project.
    
Note: This project is build using VS 2019 and .net core 3.1.

# Docker
To run the solution as a container in Docker, follow these steps:
  - Run docker-build.bat (folder Engie.GEM\Engie.GEM.ProductionPlan.API\Docker) to build a docker image. Note this image is build from the base image mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1903 which required docker to be run for Windows container.
  - Run up-docker.bat to create and start the container. Optionally you can modify the docker-compose.yml file to update the log volume where logs are saved.
  - Test the api using the endpoint: POST localhost:8008/PowerProduction
