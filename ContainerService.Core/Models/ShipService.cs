using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ContainerService.Core.Models
{
	public class ShipService
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		[Key]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name { get; set; }
	}
}
