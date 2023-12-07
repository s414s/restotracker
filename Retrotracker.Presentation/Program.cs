// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Retrotracker.Application;

Console.WriteLine("Hello, World!");

var serviceCollection = new ServiceCollection();
serviceCollection.AddTransient<IUserServices, UserServices>();
serviceCollection.AddTransient<IOrderServices, OrderServices>();
serviceCollection.AddTransient<IDishServices, DishServices>();