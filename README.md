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

## TODO LIST
- [ ] CREATE Order
- [ ] CHANGE Order
- [ ] REMOVE Order