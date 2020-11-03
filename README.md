# plantuml-service
project for SWE

a plantuml interface, that converts a valid [plantuml](https://plantuml.com/) string into a png

starts two docker instances, one with the plantuml.jar running and one with the microservice running

the microservice exposes an endpoint on port 8080

`GET /plantuml` - expects a body with form-data

* Key: `code`
* Value: valid plantuml string
* Content Type: `text/plain`

# deployment
- check out master branch
- start with 
```
docker-compose up
```
