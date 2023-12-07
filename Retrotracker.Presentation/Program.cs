// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Retrotracker.Application;
using Retrotracker.DataAccess;
using Retrotracker.Domain;

Console.WriteLine("Hello, World!");

var serviceCollection = new ServiceCollection();
serviceCollection.AddTransient<IUserServices, UserServices>();
serviceCollection.AddTransient<IOrderServices, OrderServices>();
serviceCollection.AddTransient<IDishServices, DishServices>();

serviceCollection.AddSingleton<IRepository<User>, RepositoryUsersPersistent>();
serviceCollection.AddSingleton<IRepository<Order>, RepositoryOrdersPersistent>();
serviceCollection.AddSingleton<IRepository<Ingredient>, RepositoryIngredientsPersistent>();
serviceCollection.AddSingleton<IRepository<Dish>, RepositoryDishesPersistent>();
