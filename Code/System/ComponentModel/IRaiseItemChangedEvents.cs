using System;

namespace System.ComponentModel
{
	/// <summary>Indicates whether a class converts property change events to <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> events.</summary>
	// Token: 0x0200016C RID: 364
	public interface IRaiseItemChangedEvents
	{
		/// <summary>Gets a value indicating whether the <see cref="T:System.ComponentModel.IRaiseItemChangedEvents" /> object raises <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> events.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.IRaiseItemChangedEvents" /> object raises <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> events when one of its property values changes; otherwise, false.</returns>
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000CC9 RID: 3273
		bool RaisesItemChangedEvents { get; }
	}
}
