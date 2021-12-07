using System;

namespace System.ComponentModel
{
	/// <summary>Provides the ability to retrieve the full nested name of a component.</summary>
	// Token: 0x0200015E RID: 350
	public interface INestedSite : IServiceProvider, ISite
	{
		/// <summary>Gets the full name of the component in this site.</summary>
		/// <returns>The full name of the component in this site.</returns>
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000C99 RID: 3225
		string FullName { get; }
	}
}
