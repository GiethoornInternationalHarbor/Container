using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ContainerService.Core.Models
{
	public struct ShipDockedEventModel
	{
		[Required]
		public Guid ShipId { get; set; }

		[Required]
		public Guid CustomerId { get; set; }

		/// <summary>
		/// Gets or sets the containers that are on the ship.
		/// </summary>
		public List<Container> Containers { get; set; }
	}
}
