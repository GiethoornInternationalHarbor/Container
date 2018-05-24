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
		private readonly ContainerDbContextFactory _containerDbContextFactory;

		public ContainerRepository(ContainerDbContextFactory containerDbContextFactory)
		{
			_containerDbContextFactory = containerDbContextFactory;
		}

		public async Task<Container> CreateContainerAsync(Container container)
		{
			ContainerDbContext dbContext = _containerDbContextFactory.CreateDbContext();
			var containerToAdd = (await dbContext.Containers.AddAsync(container)).Entity;
			await dbContext.SaveChangesAsync();
			return containerToAdd;
		}

		public async Task DeleteContainerAsync(Guid id)
		{
			ContainerDbContext dbContext = _containerDbContextFactory.CreateDbContext();
			var shipToDelete = new Container() { Id = id };
			dbContext.Entry(shipToDelete).State = EntityState.Deleted;
			await dbContext.SaveChangesAsync();
		}

		public async Task<Container> LoadContainerAsync(Container container)
		{
			return await Task.Run(() => container);
		}

		public async Task<Container[]> SortContainersAsync(Container[] containers)
		{
			containers.OrderBy(x => (int)(x.ContainerType)).ToArray();

			return await Task.Run(() => containers);
		}

		public async Task<Container> UnloadContainerAsync(Container container)
		{
			return await Task.Run(() => container);
		}
	}
}
