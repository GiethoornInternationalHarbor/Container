using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContainerService.Core.Models
{
	public class Container
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		[Required]
		[Key]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the serial shipping container code.
		/// </summary>
		public string SerialShippingContainerCode { get; set; }

		/// <summary>
		/// Gets or sets the products.
		/// </summary>
		public List<Product> Products { get; set; }

		/// <summary>
		/// Gets or sets the type of the product.
		/// </summary>
		/// <value>
		/// The type of the product.
		/// </value>
		public ProductType ProductType { get; set; }
	}
}
