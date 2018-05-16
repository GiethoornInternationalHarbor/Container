using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using ContainerService.Core.Messaging;
using ContainerService.Infrastructure.Messaging;
using ContainerService.Infrastructure.Database;
using ContainerService.Core.Repositories;
using ContainerService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ContainerService.Infrastructure.DI
{
	public static class DIHelper
	{
		public static void Setup(IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ContainerDbContext>(opt => opt.UseSqlServer(configuration.GetSection("DB_CONNECTION_STRING").Value));

			services.AddTransient<IContainerRepository, ContainerRepository>();
			services.AddTransient<IShipRepository, ShipRepository>();
			services.AddTransient<ITruckRepository, TruckRepository>();

			services.AddSingleton<IMessageHandler, RabbitMQMessageHandler>((provider) => new RabbitMQMessageHandler(configuration.GetSection("AMQP_URL").Value));
			services.AddTransient<IMessagePublisher, RabbitMQMessagePublisher>((provider) => new RabbitMQMessagePublisher(configuration.GetSection("AMQP_URL").Value));
		}

		public static void OnServicesSetup(IServiceProvider serviceProvider)
		{
			Console.WriteLine("Connecting to database and migrating if required");
			var dbContext = serviceProvider.GetService<ContainerDbContext>();
			dbContext.Database.Migrate();
			Console.WriteLine("Completed connecting to database");
		}
	}
}
