# Game Store
This is a simple game store catalog website where you can perform basic CRUD operations on a SQLite database. The project is built using ASP.NET and Blazor, and I added Docker to make deployment easier. I'm planning to take what I learned from this project and apply it to a future DnD web app.
### Credits 
I followed along to a youtube course to make this project, all code credit goes to **Julio Casal**. The videos can be found here:   
https://www.youtube.com/watch?v=AhAxLiGC7Pc  
https://www.youtube.com/watch?v=RBVIclt4sOo&t=17010s  

# How To Run Locally 
First cd into the api, and run it
```console
cd GameStore/GameStore.Api/
dotnet run
```
### Open a new Terminal  
cd in to the frontend, and run it
```console
cd GameStore/GameStore.Frontend/
dotnet run
````
# How To Run Using Docker
First build the project with this command
```console
cd GameStore
docker compose up --build
````
Then you can run the project using this command
```console
docker compose up
````
When you're finished, breakdown the containers using this command
```console
docker compose down
````
# Accessing The Project
The Frontend will be at
```console
http://localhost:5001
````
The Api will be at 
```console
http://localhost:5000
````  
