using System;

namespace System.ComponentModel
{
	/// <summary>Provides a base class for the container filter service.</summary>
	// Token: 0x020000E3 RID: 227
	public abstract class ContainerFilterService
	{
		/// <summary>Filters the component collection.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.ComponentCollection" /> that represents a modified collection.</returns>
		/// <param name="components">The component collection to filter.</param>
		// Token: 0x06000992 RID: 2450 RVA: 0x0001BCB8 File Offset: 0x00019EB8
		public virtual ComponentCollection FilterComponents(ComponentCollection components)
		{
			return components;
		}
	}
}
