using System.ComponentModel.DataAnnotations;

namespace ContainerService.Core.Models
{
	public class Truck
	{
		/// <summary>
		/// Gets or sets the license plate.
		/// </summary>
		[Key]
		public string LicensePlate { get; set; }

		/// <summary>
		/// Gets or sets the container.
		/// </summary>
		public Container Container { get; set; }
	}
}
