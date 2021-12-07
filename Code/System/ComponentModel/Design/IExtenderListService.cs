using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface that can list extender providers.</summary>
	// Token: 0x02000116 RID: 278
	public interface IExtenderListService
	{
		/// <summary>Gets the set of extender providers for the component.</summary>
		/// <returns>An array of type <see cref="T:System.ComponentModel.IExtenderProvider" /> that lists the active extender providers. If there are no providers, an empty array is returned.</returns>
		// Token: 0x06000AF3 RID: 2803
		IExtenderProvider[] GetExtenderProviders();
	}
}
