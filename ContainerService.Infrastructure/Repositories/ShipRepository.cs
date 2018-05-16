using ContainerService.Core.Models;
using ContainerService.Core.Repositories;
using ContainerService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ContainerService.Infrastructure.Repositories
{
	public class ShipRepository : IShipRepository
	{
		private readonly ContainerDbContext _containerDbContext;

		public ShipRepository(ContainerDbContext containerDbContext)
		{
			_containerDbContext = containerDbContext;
		}

		public async Task<Ship> CreateShip(Ship ship)
		{
			var shipToAdd = (await _containerDbContext.Ships.AddAsync(ship)).Entity;
			await _containerDbContext.SaveChangesAsync();
			return shipToAdd;
		}

		public async Task DeleteShip(Guid id)
		{
			var shipToDelete = new Ship() { Id = id };
			_containerDbContext.Entry(shipToDelete).State = EntityState.Deleted;
			await _containerDbContext.SaveChangesAsync();
		}
	}
}
