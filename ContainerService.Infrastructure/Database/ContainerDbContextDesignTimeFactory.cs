using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ContainerService.Infrastructure.Database
{
#if DEBUG

	public class ContainerDbContextDesignTimeFactory : IDesignTimeDbContextFactory<ContainerDbContext>
	{
		public ContainerDbContext CreateDbContext(string[] args)
		{
			var optBuilder = new DbContextOptionsBuilder<ContainerDbContext>();
			optBuilder.UseSqlServer("Server=.\\SQL_2017;Database=InvoiceService;Trusted_Connection=True;MultipleActiveResultSets=true");

			return new ContainerDbContext(optBuilder.Options);
		}
	}
#endif
}
