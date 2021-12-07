using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for adding and removing extender providers at design time.</summary>
	// Token: 0x02000117 RID: 279
	public interface IExtenderProviderService
	{
		/// <summary>Adds the specified extender provider.</summary>
		/// <param name="provider">The extender provider to add. </param>
		// Token: 0x06000AF4 RID: 2804
		void AddExtenderProvider(IExtenderProvider provider);

		/// <summary>Removes the specified extender provider.</summary>
		/// <param name="provider">The extender provider to remove. </param>
		// Token: 0x06000AF5 RID: 2805
		void RemoveExtenderProvider(IExtenderProvider provider);
	}
}
