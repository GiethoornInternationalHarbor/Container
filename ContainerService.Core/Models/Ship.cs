using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContainerService.Core.Models
{
	public class Ship
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		[Required]
		[Key]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the customer identifier.
		/// </summary>
		/// <value>
		/// The customer identifier.
		/// </value>
		public Guid CustomerId { get; set; }

		/// <summary>
		/// Gets or sets the containers.
		/// </summary>
		public List<Container> Containers { get; set; }
	}
}
