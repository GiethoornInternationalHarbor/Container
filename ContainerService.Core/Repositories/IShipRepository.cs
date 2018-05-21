using ContainerService.Core.Models;
using System;
using System.Threading.Tasks;

namespace ContainerService.Core.Repositories
{
	public interface IShipRepository
	{
		/// <summary>
		/// Creates the ship.
		/// </summary>
		/// <param name="ship">The ship.</param>
		/// <returns></returns>
		Task<Ship> CreateShip(Ship ship);

		/// <summary>
		/// Gets the ship.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		Task<Ship> GetShip(Guid id);

		/// <summary>
		/// Deletes the ship.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		Task DeleteShip(Guid id);
	}
}
