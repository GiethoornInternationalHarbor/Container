using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

#if DEBUG
namespace ContainerService.Infrastructure.Database
{
	public class ContainerDbContextFactory : IDesignTimeDbContextFactory<ContainerDbContext>
	{
		public ContainerDbContext CreateDbContext(string[] args)
		{
			var optBuilder = new DbContextOptionsBuilder<ContainerDbContext>();
			optBuilder.UseSqlServer("Server=.\\SQL_2017;Database=ContainerService;Trusted_Connection=True;MultipleActiveResultSets=true");

			return new ContainerDbContext(optBuilder.Options);
		}
	}
}
#endif