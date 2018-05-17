using System;

namespace ContainerService.Core.Models
{
	public static class ShipServiceConstants
    {
		/// <summary>
		/// The load container identifier
		/// </summary>
		public static readonly Guid LoadContainerId = Guid.Parse("6d96ec17-9596-434d-a39f-3227cdbefda2");

		/// <summary>
		/// The unload container identifier
		/// </summary>
		public static readonly Guid UnloadContainerId = Guid.Parse("5c80d53c-91fa-4578-937d-4afb214ff960");
    }
}
