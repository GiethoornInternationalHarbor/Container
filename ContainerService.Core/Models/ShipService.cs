using System;
using System.ComponentModel.DataAnnotations;

namespace ContainerService.Core.Models
{
	public class ShipService
	{
		/// <summary>
		/// Gets or sets the customer identifier.
		/// </summary>
		public string CustomerId { get; set; }

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		[Key]
		public Guid ShipId { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public Guid ServiceId { get; set; }
	}
}
