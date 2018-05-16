using System;
using System.ComponentModel.DataAnnotations;

namespace ContainerService.Core.Models
{
	public class Product
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		[Required]
		[Key]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the type of the product.
		/// </summary>
		/// <value>
		/// The type of the product.
		/// </value>
		public ProductType ProductType { get; set; }
	}
}
