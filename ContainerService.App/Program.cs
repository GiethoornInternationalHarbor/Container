using dotenv.net;
using ContainerService.App.Messaging;
using ContainerService.Core.Messaging;
using ContainerService.Infrastructure.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading;
using Utf8Json.Resolvers;

namespace ContainerService.App
{
	class Program
	{
		public static IServiceProvider ServiceProvider { get; private set; }

		static Program()
		{
			string filePath = ".env";

#if DEBUG
			filePath = Path.Combine(AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.LastIndexOf("bin")), filePath);
#endif

			DotEnv.Config(throwOnError: false, filePath: filePath);

			//setup our DI
			var serviceCollection = new ServiceCollection();

			ConfigureServices(serviceCollection);

			ServiceProvider = serviceCollection.BuildServiceProvider();

			DIHelper.OnServicesSetup(ServiceProvider);
		}

		private static void ConfigureServices(IServiceCollection services)
		{
			IConfiguration config = new ConfigurationBuilder()
				.AddEnvironmentVariables()
				.Build();

			DIHelper.Setup(services, config);

			// Setup the app services
			services.AddTransient<IMessageHandlerCallback, ContainerMessageHandler>();
		}

		static void Main(string[] args)
		{
			CompositeResolver.RegisterAndSetAsDefault(new[]
			{
				EnumResolver.UnderlyingValue,
				StandardResolver.ExcludeNullCamelCase
			});

			// Get the message handler
			IMessageHandler messageHandler = ServiceProvider.GetService<IMessageHandler>();
			IMessageHandlerCallback messageHandlerCallback = ServiceProvider.GetService<IMessageHandlerCallback>();

			try
			{
				Console.WriteLine("Starting handler");
				messageHandler.Start(messageHandlerCallback);
				Console.WriteLine("Handler started");
			}
			catch (Exception ex)
			{
				// Error during staring
				Console.Error.WriteLine($"Error during starting message handler, message: {ex.Message}");
				Environment.Exit(1);
			}

			Console.WriteLine("Container service started.");
			while (true)
			{
				Thread.Sleep(10000);
			}
		}
	}
}