// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Retrotracker.Application;
using Retrotracker.DataAccess;
using Retrotracker.Domain;
using Retrotracker.Presentation;

Console.WriteLine("Hello, World!");

var serviceCollection = new ServiceCollection();
serviceCollection.AddTransient<IUserServices, UserServices>();
serviceCollection.AddTransient<IOrderServices, OrderServices>();
serviceCollection.AddTransient<IDishServices, DishServices>();

serviceCollection.AddSingleton<IRepository<User>, RepositoryUsersPersistent>();
serviceCollection.AddSingleton<IRepository<Order>, RepositoryOrdersPersistent>();
serviceCollection.AddSingleton<IRepository<Ingredient>, RepositoryIngredientsPersistent>();
serviceCollection.AddSingleton<IRepository<Dish>, RepositoryDishesPersistent>();

Console.WriteLine("Dependency injection done!");

// TODO - preguntar si esto iria en capa CrossCutting
RepositoryUsersPersistent repoUsers = new();
RepositoryOrdersPersistent repoOrders = new();
RepositoryIngredientsPersistent repoIngredients = new();
RepositoryDishesPersistent repoDishes = new(repoIngredients);

UserServices userServices = new(repoUsers);
OrderServices orderServices = new(repoOrders);
DishServices dishServices = new();

ConsoleLogger console = new(
    userServices,
    orderServices,
    dishServices
);

console.Run();

Console.WriteLine("Finnishing!");