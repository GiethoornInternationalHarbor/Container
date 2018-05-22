using Microsoft.EntityFrameworkCore;

namespace ContainerService.Infrastructure.Database
{
	public class ContainerDbContextFactory
	{
		/// <summary>
		/// Gets or sets the connection string.
		/// </summary>
		protected string ConnectionString { get; set; }

		public ContainerDbContextFactory(string connectionString)
		{
			ConnectionString = connectionString;
		}

		/// <summary>
		/// Creates the database context.
		/// </summary>
		/// <returns></returns>
		public ContainerDbContext CreateDbContext()
		{
			var optBuilder = new DbContextOptionsBuilder<ContainerDbContext>();
			optBuilder.UseSqlServer(ConnectionString);

			return new ContainerDbContext(optBuilder.Options);
		}
	}
}