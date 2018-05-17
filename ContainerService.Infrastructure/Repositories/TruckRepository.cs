using ContainerService.Core.Models;
using ContainerService.Core.Repositories;
using ContainerService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ContainerService.Infrastructure.Repositories
{
	public class TruckRepository : ITruckRepository
	{
		private readonly ContainerDbContext _containerDbContext;

		public TruckRepository(ContainerDbContext containerDbContext)
		{
			_containerDbContext = containerDbContext;
		}

		public async Task<Truck> CreateTruckAsync(Truck truck)
		{
			var truckToAdd = (await _containerDbContext.Trucks.AddAsync(truck)).Entity;
			await _containerDbContext.SaveChangesAsync();
			return truckToAdd;
		}

		public async Task DeleteTruckAsync(string plate)
		{
			var truckToDelete = new Truck() { LicensePlate = plate };
			_containerDbContext.Entry(truckToDelete).State = EntityState.Deleted;
			await _containerDbContext.SaveChangesAsync();
		}

		public Task<Truck> GetTruck(string plate)
		{
			return _containerDbContext.Trucks.LastOrDefaultAsync(x => x.LicensePlate == plate);
		}
	}
}
