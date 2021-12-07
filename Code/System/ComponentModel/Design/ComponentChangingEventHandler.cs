using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Represents the method that will handle a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentChangingEventArgs" /> event that contains the event data. </param>
	// Token: 0x020004FB RID: 1275
	// (Invoke) Token: 0x06002C98 RID: 11416
	[ComVisible(true)]
	public delegate void ComponentChangingEventHandler(object sender, ComponentChangingEventArgs e);
}
