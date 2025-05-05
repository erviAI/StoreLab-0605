using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using StoreLab.ApplicationCore.Infrastructure;
using StoreLab.ApplicationCore.Interfaces;
using StoreLab.ApplicationCore.Models;
using StoreLab.ApplicationCore.Services;
using StoreLab.RetroStore;
using StoreLab.RetroStore.Data;

var services = new ServiceCollection();

var configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appSettings.json", true)
.Build();

services.AddSingleton<IConfiguration>(configuration);

Func<List<CatalogItem>> catalogItems = DataHelper.GetCatalogItems;
services.AddSingleton(catalogItems);
services.AddScoped<ICatalogService, CatalogService>();
services.AddScoped<ICatalogRepository, InMemoryCatalogRepository>();
services.AddScoped<IBasketService, BasketService>();
services.AddScoped<IBasketRepository, InMemoryBasketRepository>();

services.AddSingleton<ConsoleApp>();

var servicesProvider = services.BuildServiceProvider();

var consoleApp = servicesProvider.GetRequiredService<ConsoleApp>();
consoleApp.Run().Wait();