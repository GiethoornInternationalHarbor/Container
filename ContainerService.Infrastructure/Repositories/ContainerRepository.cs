using ContainerService.Core.Models;
using ContainerService.Core.Repositories;
using ContainerService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace ContainerService.Infrastructure.Repositories
{
	public class ContainerRepository : IContainerRepository
	{
		private readonly ContainerDbContext _containerDbContext;

		public ContainerRepository(ContainerDbContext containerDbContext)
		{
			_containerDbContext = containerDbContext;
		}

		public async Task<Container> CreateContainerAsync(Container container)
		{
			var containerToAdd = (await _containerDbContext.Containers.AddAsync(container)).Entity;
			await _containerDbContext.SaveChangesAsync();
			return containerToAdd;
		}

		public async Task DeleteContainerAsync(Guid id)
		{
			var shipToDelete = new Core.Models.Container() { Id = id };
			_containerDbContext.Entry(shipToDelete).State = EntityState.Deleted;
			await _containerDbContext.SaveChangesAsync();
		}

		public async Task<Container> LoadContainerAsync(Container container)
		{
			return await Task.Run(() => container);
		}

		public async Task<Container[]> SortContainersAsync(Container[] containers)
		{
			containers.OrderByDescending(x => (int)(x.ProductType)).ToArray();

			return await Task.Run(() => containers);
		}

		public async Task<Container> UnloadContainerAsync(Container container)
		{
			return await Task.Run(() => container);
		}
	}
}
