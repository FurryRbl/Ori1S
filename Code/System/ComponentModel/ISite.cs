using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Provides functionality required by sites.</summary>
	// Token: 0x0200016D RID: 365
	[ComVisible(true)]
	public interface ISite : IServiceProvider
	{
		/// <summary>Gets the component associated with the <see cref="T:System.ComponentModel.ISite" /> when implemented by a class.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> instance associated with the <see cref="T:System.ComponentModel.ISite" />.</returns>
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000CCA RID: 3274
		IComponent Component { get; }

		/// <summary>Gets the <see cref="T:System.ComponentModel.IContainer" /> associated with the <see cref="T:System.ComponentModel.ISite" /> when implemented by a class.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IContainer" /> instance associated with the <see cref="T:System.ComponentModel.ISite" />.</returns>
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000CCB RID: 3275
		IContainer Container { get; }

		/// <summary>Determines whether the component is in design mode when implemented by a class.</summary>
		/// <returns>true if the component is in design mode; otherwise, false.</returns>
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000CCC RID: 3276
		bool DesignMode { get; }

		/// <summary>Gets or sets the name of the component associated with the <see cref="T:System.ComponentModel.ISite" /> when implemented by a class.</summary>
		/// <returns>The name of the component associated with the <see cref="T:System.ComponentModel.ISite" />; or null, if no name is assigned to the component.</returns>
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000CCD RID: 3277
		// (set) Token: 0x06000CCE RID: 3278
		string Name { get; set; }
	}
}
