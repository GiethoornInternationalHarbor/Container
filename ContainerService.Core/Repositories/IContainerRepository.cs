using ContainerService.Core.Models;
using System;
using System.Threading.Tasks;

namespace ContainerService.Core.Repositories
{
	public interface IContainerRepository
	{
		/// <summary>
		/// Creates the container asynchronous.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <returns></returns>
		Task<Container> CreateContainerAsync(Container container);

		/// <summary>
		/// Unloads the containers asynchronous.
		/// </summary>
		/// <param name="containers">The containers.</param>
		/// <returns></returns>
		Task<Container> UnloadContainerAsync(Container container);

		/// <summary>
		/// Loads the containers asynchronous.
		/// </summary>
		/// <param name="containers">The containers.</param>
		/// <returns></returns>
		Task<Container> LoadContainerAsync(Container containers);

		/// <summary>
		/// Sorts the containers asynchronous.
		/// </summary>
		/// <param name="containers">The containers.</param>
		/// <returns></returns>
		Task<Container> SortContainerAsync(Container containers);

		/// <summary>
		/// Deletes the container asynchronous.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		Task DeleteContainerAsync(Guid id);
	}
}
