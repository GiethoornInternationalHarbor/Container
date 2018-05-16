using System.Collections.Generic;
using System.ComponentModel;
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
		Task<Container> UnloadContainersAsync(List<Models.Container> containers);

		/// <summary>
		/// Loads the containers asynchronous.
		/// </summary>
		/// <param name="containers">The containers.</param>
		/// <returns></returns>
		Task<Container> LoadContainersAsync(List<Models.Container> containers);

		/// <summary>
		/// Sorts the containers asynchronous.
		/// </summary>
		/// <param name="containers">The containers.</param>
		/// <returns></returns>
		Task<Container> SortContainersAsync(List<Models.Container> containers);
	}
}
