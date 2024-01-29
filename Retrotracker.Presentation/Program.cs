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
serviceCollection.AddSingleton<IRepositoryIngredients, RepositoryIngredientsPersistent>();
serviceCollection.AddSingleton<IRepository<Dish>, RepositoryDishesPersistent>();

// Services
serviceCollection.AddTransient<IUserServices, UserServices>();
serviceCollection.AddTransient<IOrderServices, OrderServices>();
serviceCollection.AddTransient<IDishServices, DishServices>();

serviceCollection.AddTransient<IMenuPrinter, MenuPrinter>();

var serviceProvider = serviceCollection.BuildServiceProvider();

// Resolve an instance of IConsoleLogger
var menuPrinter = serviceProvider.GetService<IMenuPrinter>();
menuPrinter?.Run();
