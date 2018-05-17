using ContainerService.Core.Models;
using System.Threading.Tasks;

namespace ContainerService.Core.Repositories
{
	public interface ITruckRepository
	{
		/// <summary>
		/// Gets the truck.
		/// </summary>
		/// <param name="truck">The truck.</param>
		/// <returns></returns>
		Task<Truck> GetTruck(string plate);

		/// <summary>
		/// Creates the truck asynchronous.
		/// </summary>
		/// <param name="truck">The truck.</param>
		/// <returns></returns>
		Task<Truck> CreateTruckAsync(Truck truck);

		/// <summary>
		/// Deletes the truck asynchronous.
		/// </summary>
		/// <param name="plate">The plate.</param>
		/// <returns></returns>
		Task DeleteTruckAsync(string plate);
	}
}
