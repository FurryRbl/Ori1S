using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentAdding" />, <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentAdded" />, <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRemoving" />, and <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRemoved" /> events raised for component-level events.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentEventArgs" /> that contains the event data. </param>
	// Token: 0x020004FC RID: 1276
	// (Invoke) Token: 0x06002C9C RID: 11420
	[ComVisible(true)]
	public delegate void ComponentEventHandler(object sender, ComponentEventArgs e);
}
