// See https://aka.ms/new-console-template for more information
// git checkout <hash del commit> -> para viajar a otro commit anterior
// gitk - herramienta para ver los cambios
// fastforward vs squas commit
// git bisec
// git blame
// git reflog -> te salva la vida
// git status
// git stash
// git checkout -> tambien puede ser para ir a un commit anterior

using Microsoft.Extensions.DependencyInjection;
using Retrotracker.Application;
using Retrotracker.DataAccess;
using Retrotracker.Domain;
using Retrotracker.Presentation;

var serviceCollection = new ServiceCollection();

// Registering services
// Repositories
serviceCollection.AddSingleton<IRepository<User>, RepositoryUsersPersistent>();
serviceCollection.AddSingleton<IRepository<Order>, RepositoryOrdersPersistent>();
serviceCollection.AddSingleton<IRepository<Ingredient>, RepositoryIngredientsPersistent>();
serviceCollection.AddSingleton<IRepository<Dish>, RepositoryDishesPersistent>();

// Services
serviceCollection.AddTransient<IUserServices, UserServices>();
serviceCollection.AddTransient<IOrderServices, OrderServices>();
serviceCollection.AddTransient<IDishServices, DishServices>();

var serviceProvider = serviceCollection.BuildServiceProvider();

// Resolve an instance of IUserServices
// var consoleLogger = serviceProvider.GetService<IBankAccountService>();

Console.WriteLine("Dependency injections done!");

// TODO - preguntar si esto iria en capa CrossCutting
// Manual dependency injection 
RepositoryUsersPersistent repoUsers = new();
RepositoryOrdersPersistent repoOrders = new();
RepositoryIngredientsPersistent repoIngredients = new();
RepositoryDishesPersistent repoDishes = new(repoIngredients);

UserServices userServices = new(repoUsers);
OrderServices orderServices = new(repoOrders);
DishServices dishServices = new(repoDishes);

ConsoleLogger console = new( userServices, orderServices, dishServices);
console.Run();