using ContainerService.Core.Messaging;
using System.Threading.Tasks;

namespace ContainerService.App.Messaging
{
	public class ContainerMessageHandler : IMessageHandlerCallback
	{

		public ContainerMessageHandler(IMessagePublisher messagePublisher)
		{

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

			return true;
		}

		private async Task<bool> HandleShipUndocked(string message)
		{

			return true;
		}

		private async Task<bool> HandleTruckArrived(string message)
		{

			return true;
		}
	}
}
