using ContainerService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ContainerService.Infrastructure.Database
{
	public class ContainerDbContext : DbContext
	{
		public ContainerDbContext(DbContextOptions options)
			: base(options)
		{ }

		public DbSet<Container> Containers { get; set; }

		public DbSet<Ship> Ships { get; set; }

		public DbSet<Truck> Trucks { get; set; }
	}
}