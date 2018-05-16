using System.ComponentModel.DataAnnotations;

namespace ContainerService.Core.Models
{
	public class Truck
	{
		[Key]
		public string LicensePlate { get; set; }
	}
}
