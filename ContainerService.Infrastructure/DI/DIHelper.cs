using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using ContainerService.Core.Messaging;
using ContainerService.Infrastructure.Messaging;

namespace ContainerService.Infrastructure.DI
{
	public static class DIHelper
	{
		public static void Setup(IServiceCollection services, IConfiguration configuration)
		{


			services.AddSingleton<IMessageHandler, RabbitMQMessageHandler>((provider) => new RabbitMQMessageHandler(configuration.GetSection("AMQP_URL").Value));
			services.AddTransient<IMessagePublisher, RabbitMQMessagePublisher>((provider) => new RabbitMQMessagePublisher(configuration.GetSection("AMQP_URL").Value));
		}

		public static void OnServicesSetup(IServiceProvider serviceProvider)
		{

		}
	}
}
