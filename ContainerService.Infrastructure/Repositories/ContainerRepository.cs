using ContainerService.Core.Models;
using ContainerService.Core.Repositories;
using ContainerService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

		public async Task<List<Container>> LoadContainersAsync(List<Container> containers)
		{
			return await Task.Run(() => containers);
		}

		public async Task<List<Container>> SortContainersAsync(List<Container> containers)
		{
			return await Task.Run(() => containers);
		}

		public async Task<List<Container>> UnloadContainersAsync(List<Container> containers)
		{
			return await Task.Run(() => containers);
		}
	}
}
