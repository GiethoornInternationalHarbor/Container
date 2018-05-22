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
		private readonly ContainerDbContextFactory _containerDbContextFactory;

		public ShipRepository(ContainerDbContextFactory containerDbContextFactory)
		{
			_containerDbContextFactory = containerDbContextFactory;
		}

		public async Task<Ship> CreateShip(Ship ship)
		{
			ContainerDbContext dbContext = _containerDbContextFactory.CreateDbContext();
			var shipToAdd = (await dbContext.Ships.AddAsync(ship)).Entity;
			await dbContext.SaveChangesAsync();
			return shipToAdd;
		}

		public async Task DeleteShip(Guid id)
		{
			ContainerDbContext dbContext = _containerDbContextFactory.CreateDbContext();
			var shipToDelete = new Ship() { Id = id };
			dbContext.Entry(shipToDelete).State = EntityState.Deleted;
			await dbContext.SaveChangesAsync();
		}

		public Task<Ship> GetShip(Guid id)
		{
			ContainerDbContext dbContext = _containerDbContextFactory.CreateDbContext();
			return dbContext.Ships.LastOrDefaultAsync(x => x.Id == id);
		}
	}
}
