using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using ContainerService.Core.Messaging;
using ContainerService.Infrastructure.Messaging;
using ContainerService.Infrastructure.Database;
using ContainerService.Core.Repositories;
using ContainerService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace ContainerService.Infrastructure.DI
{
	public static class DIHelper
	{
		public static void Setup(IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton((provider) => new ContainerDbContextFactory(configuration.GetSection("DB_CONNECTION_STRING").Value));

			services.AddTransient<IContainerRepository, ContainerRepository>();
			services.AddTransient<IShipRepository, ShipRepository>();
			services.AddTransient<ITruckRepository, TruckRepository>();

			services.AddSingleton<IMessageHandler, RabbitMQMessageHandler>((provider) => new RabbitMQMessageHandler(configuration.GetSection("AMQP_URL").Value));
			services.AddTransient<IMessagePublisher, RabbitMQMessagePublisher>((provider) => new RabbitMQMessagePublisher(configuration.GetSection("AMQP_URL").Value));
		}

		public static void OnServicesSetup(IServiceProvider serviceProvider)
		{
			Console.WriteLine("Connecting to database and migrating if required");
			var dbContextFactory = serviceProvider.GetService<ContainerDbContextFactory>();
			ContainerDbContext dbContext = dbContextFactory.CreateDbContext();
			Policy
			 .Handle<Exception>()
			 .WaitAndRetry(9, r => TimeSpan.FromSeconds(5), (ex, ts) =>
			 {
				 Console.Error.WriteLine("Error connecting to database. Retrying in 5 sec.");
			 })
			 .Execute(() =>
			 {
				 dbContext.Database.Migrate();
				 Console.WriteLine("Completed connecting to database");
			 });
		}
	}
}
