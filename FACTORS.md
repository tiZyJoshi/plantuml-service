[12factor.net](https://12factor.net/)

### 1 Codebase - One codebase tracked in revision control, many deploys
This git repo is the single codebase for the whole project.
There is a branch 'deploy' to publish changes to docker hub. (The master branch uses this [dockerhubimage](https://hub.docker.com/r/tizyjoshi/plantumlservice))
### 2 Dependencies - Explicitly declare and isolate dependencies
When started from 'master' branch all required resources are downloaded from docker hub.
On 'Deploy' branch the build is done in a docker container and can be published to docker hub.
The plantuml-service image comes as a precompiled container.
### 3 Config - Store config in the environment
Docker is setup in a way that currently the app needs no configuration.
### 4 Backing services - Treat backing services as attached resources
Docker-compose can setup the plantuml runtime dependency.
### 5 Build, release, run - Strictly separate build and run stages
Build is done in 'develop' branch
Release and Run are pretty much the same since no configuration takes place
### 6 Processes - Execute the app as one or more stateless processes
Since all components are spawned in different docker containers, this should be fulfilled.
### 7 Port binding - Export services via port binding
ASP.net web api applications force this behaviour
### 8 Concurrency - Scale out via the process model
The 'PlantUMLController.Get()' Method is an async method.
But currently only one plantuml instance is spawned, which, as far as ive tested does not support multiple requests at once.
Spawning multiple plantuml instances would solve this problem.
### 9 Disposability - Maximize robustness with fast startup and graceful shutdown
Service is completely stateless, so it doesnt even leave any traces in the docker container
Docker does the rest (hoefully)
### 10 Dev/prod parity - Keep development, staging, and production as similar as possible
The only difference between these stages is the docker compose file.
All Code is managed by Visual Studio, so it applies to all the VS rules and works beautifully in the IDE.
dev/deploy can both be handled through the VS UI.
### 11 Logs - Treat logs as event streams
ASP.Net Standard Logger does so by default.
### 12 Admin processes - Run admin/management tasks as one-off processes
No admin processes
