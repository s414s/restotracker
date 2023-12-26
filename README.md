# restotracker
Restrotracker es una aplicación que permite el control de stock y comandas en los restaurantes. Mediante un sistema de publicación de tareas, la cocina puede recibir ordenes para la preparación de platos.

Comandos de interés:
```
dotnet new sln
dotnet new console -n MyConsoleApp
dotnet sln add MyConsoleApp/MyConsoleApp.csproj
dotnet build
dotnet run --project .\Retrotracker.Presentation\Retrotracker.Presentation.csproj

dotnet add reference path/to/ProjectA/ProjectA.csproj

dotnet add package Microsoft.Extensions.DependencyInjection --version 8.0.0


dotnet run --project .\Retrotracker.Presentation\Retrotracker.Presentation.csproj
```

Docker
https://learn.microsoft.com/es-es/dotnet/core/docker/build-container?tabs=windows&pivots=dotnet-8-0

docker build --rm -t new-net/cloud-retro:latest .

docker image ls | grap cloud-retro

docker run --rm -p 5000:5000 -p 5001:5001 -e ASPNET_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5001 new-net/cloud-retro