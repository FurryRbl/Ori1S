using System;

namespace System.ComponentModel
{
	/// <summary>Provides functionality for nested containers, which logically contain zero or more other components and are owned by a parent component.</summary>
	// Token: 0x0200015D RID: 349
	public interface INestedContainer : IDisposable, IContainer
	{
		/// <summary>Gets the owning component for the nested container.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> that owns the nested container.</returns>
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000C98 RID: 3224
		IComponent Owner { get; }
	}
}
