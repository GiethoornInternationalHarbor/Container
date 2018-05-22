using ContainerService.Core.Models;
using ContainerService.Core.Repositories;
using ContainerService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContainerService.Infrastructure.Repositories
{
	public class TruckRepository : ITruckRepository
	{
		private readonly ContainerDbContextFactory _containerDbContextFactory;

		public TruckRepository(ContainerDbContextFactory containerDbContextFactory)
		{
			_containerDbContextFactory = containerDbContextFactory;
		}

		public async Task<Truck> CreateTruckAsync(Truck truck)
		{
			ContainerDbContext dbContext = _containerDbContextFactory.CreateDbContext();
			var truckToAdd = (await dbContext.Trucks.AddAsync(truck)).Entity;
			await dbContext.SaveChangesAsync();
			return truckToAdd;
		}

		public async Task DeleteTruckAsync(string plate)
		{
			ContainerDbContext dbContext = _containerDbContextFactory.CreateDbContext();
			var truckToDelete = new Truck() { LicensePlate = plate };
			dbContext.Entry(truckToDelete).State = EntityState.Deleted;
			await dbContext.SaveChangesAsync();
		}

		public Task<Truck> GetTruck(string plate)
		{
			ContainerDbContext dbContext = _containerDbContextFactory.CreateDbContext();
			return dbContext.Trucks.LastOrDefaultAsync(x => x.LicensePlate == plate);
		}

		public Task<List<Truck>> GetTrucks()
		{
			ContainerDbContext dbContext = _containerDbContextFactory.CreateDbContext();
			return dbContext.Trucks.Where(x => x.Container == null).ToListAsync();
		}
	}
}
