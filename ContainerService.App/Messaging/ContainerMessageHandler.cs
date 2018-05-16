using ContainerService.Core.Messaging;
using ContainerService.Core.Models;
using ContainerService.Core.Repositories;
using System.Threading.Tasks;
using Utf8Json;

namespace ContainerService.App.Messaging
{
	public class ContainerMessageHandler : IMessageHandlerCallback
	{
		private readonly IShipRepository _shipRepository;
		private readonly IContainerRepository _containerRepository;
		private readonly ITruckRepository _truckRepository;
		private readonly IMessagePublisher _messagePublisher;

		public ContainerMessageHandler(IShipRepository shipRepository,
									   IContainerRepository containerRepository,
									   ITruckRepository truckRepository,
									   IMessagePublisher messagePublisher)
		{
			_shipRepository = shipRepository;
			_containerRepository = containerRepository;
			_truckRepository = truckRepository;
			_messagePublisher = messagePublisher;
		}

		public async Task<bool> HandleMessageAsync(MessageTypes messageType, string message)
		{
			switch (messageType)
			{
				case MessageTypes.ShipDocked:
					{
						return await HandleShipDocked(message);
					}
				case MessageTypes.ShipUndocked:
					{
						return await HandleShipUndocked(message);
					}
				case MessageTypes.TruckArrived:
					{
						return await HandleTruckArrived(message);
					}
			}

			return true;
		}

		private async Task<bool> HandleShipDocked(string message)
		{
			var receivedShip = JsonSerializer.Deserialize<Ship>(message);

			await _shipRepository.CreateShip(receivedShip);

			foreach (var container in receivedShip.Containers)
			{
				await _containerRepository.CreateContainerAsync(container);
			}

			await _containerRepository.UnloadContainersAsync(receivedShip.Containers);

			await _messagePublisher.PublishMessageAsync(MessageTypes.ShipContainerUnloaded, receivedShip);

			await _containerRepository.SortContainersAsync(receivedShip.Containers);

			await _containerRepository.LoadContainersAsync(receivedShip.Containers);

			await _messagePublisher.PublishMessageAsync(MessageTypes.ShipContainerLoaded, receivedShip);

			await _messagePublisher.PublishMessageAsync(MessageTypes.ServiceCompleted, receivedShip.ShipService);

			return true;
		}

		private async Task<bool> HandleShipUndocked(string message)
		{
			var receivedShip = JsonSerializer.Deserialize<Ship>(message);

			foreach (var container in receivedShip.Containers)
			{
				await _containerRepository.DeleteContainerAsync(container.Id);
			}

			await _shipRepository.DeleteShip(receivedShip.Id);

			return true;
		}

		private async Task<bool> HandleTruckArrived(string message)
		{
			var receivedTruck = JsonSerializer.Deserialize<Truck>(message);

			await _truckRepository.CreateTruckAsync(receivedTruck);

			return true;
		}
	}
}
