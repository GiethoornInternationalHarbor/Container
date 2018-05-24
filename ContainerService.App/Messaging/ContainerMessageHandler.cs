using ContainerService.Core.Messaging;
using ContainerService.Core.Models;
using ContainerService.Core.Repositories;
using System;
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
			var receivedShip = JsonSerializer.Deserialize<ShipDockedEventModel>(message);

			Ship ship = new Ship()
			{
				Containers = receivedShip.Containers,
				CustomerId = receivedShip.CustomerId,
				Id = receivedShip.ShipId
			};

			Ship createdShip = await _shipRepository.CreateShip(ship);

			foreach (var container in createdShip.Containers)
			{
				await _containerRepository.CreateContainerAsync(container);
			}

			return true;
		}

		private async Task<bool> HandleShipUndocked(string message)
		{
			var receivedShip = JsonSerializer.Deserialize<ShipDockedEventModel>(message);

			Ship ship = new Ship()
			{
				Containers = receivedShip.Containers,
				Id = receivedShip.ShipId
			};

			foreach (var container in ship.Containers)
			{
				await _containerRepository.DeleteContainerAsync(container.Id);
			}

			await _shipRepository.DeleteShip(ship.Id);

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
			var receivedShipService = JsonSerializer.Deserialize<ServiceRequest>(message);
			var ship = await _shipRepository.GetShip(receivedShipService.ShipId);

			if (ship != null)
			{
				var trucks = await _truckRepository.GetTrucks();

				var containersToSort = trucks.Select(x => x.Container).ToArray();

				await _containerRepository.SortContainersAsync(containersToSort);

				foreach (var truck in trucks)
				{
					var selectedTruck = trucks?.FirstOrDefault();

					if (selectedTruck != null)
					{
						if (receivedShipService.ServiceId == ShipServiceConstants.LoadContainerId)
						{
							await _containerRepository.LoadContainerAsync(selectedTruck.Container);

							await _messagePublisher.PublishMessageAsync(MessageTypes.ShipContainerLoaded, selectedTruck);

							await PublishServiceCompleteAndDeleteTruck(receivedShipService, selectedTruck);
						}

						if (receivedShipService.ServiceId == ShipServiceConstants.UnloadContainerId)
						{
							await _containerRepository.UnloadContainerAsync(selectedTruck.Container);

							await _messagePublisher.PublishMessageAsync(MessageTypes.ShipContainerUnloaded, selectedTruck);

							await PublishServiceCompleteAndDeleteTruck(receivedShipService, selectedTruck);
						}
					}
				}
			}

			return true;
		}

		private async Task<bool> PublishServiceCompleteAndDeleteTruck(ServiceRequest shipService, Truck truck)
		{
			await _messagePublisher.PublishMessageAsync(MessageTypes.ServiceCompleted, shipService);

			await _truckRepository.DeleteTruckAsync(truck.LicensePlate);

			return true;
		}
	}
}
