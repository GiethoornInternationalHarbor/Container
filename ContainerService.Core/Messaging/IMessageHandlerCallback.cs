using System.Threading.Tasks;

namespace ContainerService.Core.Messaging
{
	public interface IMessageHandlerCallback
	{
		/// <summary>
		/// Handles the message asynchronous.
		/// </summary>
		/// <param name="messageType">Type of the message.</param>
		/// <param name="message">The message.</param>
		/// <returns></returns>
		Task<bool> HandleMessageAsync(MessageTypes messageType, string message);
	}
}
