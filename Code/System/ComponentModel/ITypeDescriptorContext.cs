using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Provides contextual information about a component, such as its container and property descriptor.</summary>
	// Token: 0x02000171 RID: 369
	[ComVisible(true)]
	public interface ITypeDescriptorContext : IServiceProvider
	{
		/// <summary>Gets the container representing this <see cref="T:System.ComponentModel.TypeDescriptor" /> request.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.IContainer" /> with the set of objects for this <see cref="T:System.ComponentModel.TypeDescriptor" />; otherwise, null if there is no container or if the <see cref="T:System.ComponentModel.TypeDescriptor" /> does not use outside objects.</returns>
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000CD8 RID: 3288
		IContainer Container { get; }

		/// <summary>Gets the object that is connected with this type descriptor request.</summary>
		/// <returns>The object that invokes the method on the <see cref="T:System.ComponentModel.TypeDescriptor" />; otherwise, null if there is no object responsible for the call.</returns>
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000CD9 RID: 3289
		object Instance { get; }

		/// <summary>Gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is associated with the given context item.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes the given context item; otherwise, null if there is no <see cref="T:System.ComponentModel.PropertyDescriptor" /> responsible for the call.</returns>
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000CDA RID: 3290
		PropertyDescriptor PropertyDescriptor { get; }

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanged" /> event.</summary>
		// Token: 0x06000CDB RID: 3291
		void OnComponentChanged();

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event.</summary>
		/// <returns>true if this object can be changed; otherwise, false.</returns>
		// Token: 0x06000CDC RID: 3292
		bool OnComponentChanging();
	}
}
