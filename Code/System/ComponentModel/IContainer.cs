using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Provides functionality for containers. Containers are objects that logically contain zero or more components.</summary>
	// Token: 0x02000155 RID: 341
	[ComVisible(true)]
	public interface IContainer : IDisposable
	{
		/// <summary>Gets all the components in the <see cref="T:System.ComponentModel.IContainer" />.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.IComponent" /> objects that represents all the components in the <see cref="T:System.ComponentModel.IContainer" />.</returns>
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000C78 RID: 3192
		ComponentCollection Components { get; }

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.IComponent" /> to the <see cref="T:System.ComponentModel.IContainer" /> at the end of the list.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to add. </param>
		// Token: 0x06000C79 RID: 3193
		void Add(IComponent component);

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.IComponent" /> to the <see cref="T:System.ComponentModel.IContainer" /> at the end of the list, and assigns a name to the component.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to add. </param>
		/// <param name="name">The unique, case-insensitive name to assign to the component.-or- null that leaves the component unnamed. </param>
		// Token: 0x06000C7A RID: 3194
		void Add(IComponent component, string name);

		/// <summary>Removes a component from the <see cref="T:System.ComponentModel.IContainer" />.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to remove. </param>
		// Token: 0x06000C7B RID: 3195
		void Remove(IComponent component);
	}
}
