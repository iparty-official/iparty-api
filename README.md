# iParty API

This is an API for a future mobile app called iParty. The purpose of the app is to connect event sector suppliers to people who want to promote an event like weddings, anniversaries, and ceremonies in general.

## Outline

 - [Libraries](#libraries)
 - [Production](#production)
 - [Running locally](#running-locally)
 - [Documentation](#documentation)

## Libraries

This project is using some libraries and frameworks:

 - [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
 - [Docker](https://docs.docker.com/)
 - [Swagger](https://swagger.io/)

## Production

To see the app running in a production environment, access https://iparty-api.herokuapp.com/

## Running locally

First, clone the project to your local machine using the following command:

```
git clone https://github.com/ozmartins/iparty-api.git
```

Then, to enter into the project directory, type into the terminal:

```
cd iparty-api
```
Finally, run the app using the command shown below:

```
docker compose up -d
```

Now, the app is running and you can try it accessing the URL https://localhost:5001

## Documentation

To see API documentation, please click on this link https://iparty-api.herokuapp.com/swagger/index.html
