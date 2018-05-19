using ContainerService.Core.Messaging;
using ContainerService.Core.Models;
using ContainerService.Core.Repositories;
using System.Linq;
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
				case MessageTypes.ServiceRequested:
					{
						return await HandleServiceRequested(message);
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

		private async Task<bool> HandleServiceRequested(string message)
		{
			var receivedShipService = JsonSerializer.Deserialize<ShipService>(message);
			var trucks = await _truckRepository.GetTrucks();

			var containersToSort = trucks.Select(x => x.Container).ToArray();

			await _containerRepository.SortContainersAsync(containersToSort);	

			foreach (var truck in trucks)
			{
				var selectedTruck = trucks?.FirstOrDefault();

				if (selectedTruck != null)
				{
					if (receivedShipService.Id == ShipServiceConstants.LoadContainerId)
					{
						await _containerRepository.LoadContainerAsync(selectedTruck.Container);

						await _messagePublisher.PublishMessageAsync(MessageTypes.ShipContainerLoaded, selectedTruck);

						await PublishServiceCompleteAndDeleteTruck(receivedShipService, selectedTruck);
					}

					if (receivedShipService.Id == ShipServiceConstants.UnloadContainerId)
					{
						await _containerRepository.UnloadContainerAsync(selectedTruck.Container);

						await _messagePublisher.PublishMessageAsync(MessageTypes.ShipContainerUnloaded, selectedTruck);

						await PublishServiceCompleteAndDeleteTruck(receivedShipService, selectedTruck);
					}
				}
			}

			return true;
		}

		private async Task<bool> PublishServiceCompleteAndDeleteTruck(ShipService shipService, Truck truck)
		{
			await _messagePublisher.PublishMessageAsync(MessageTypes.ServiceCompleted, shipService);

			await _truckRepository.DeleteTruckAsync(truck.LicensePlate);

			return true;
		}
	}
}
