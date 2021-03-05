# BE-Developer-Case-Study
## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)
* [Tests](#tests)

## General info
This project is simple Rest Api Application

swagger is located on: http://localhost:5900/swagger/index.html
	
## Technologies
Project is created with:
* Net Core version: 5.0
* EF Core version: 6
* MSSQL Server
* Docker
	
## Setup
To run this project, you can use docker for product or visual studio for development:

running with docker for production:

```
$ git clone git@github.com:s3w3nofficial/BE-Developer-Case-Study.git
$ docker-compose up --build -d
```

running with VS / VS Code:

```
$ git clone git@github.com:s3w3nofficial/BE-Developer-Case-Study.git
```
clone project and then open sln file

## Tests
To run tests open command line and navigate to directory of the project and execute commands metioned below:

```
$ cd UnitTests
$ dotnet test
```
