using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface to add and remove the event handlers for events that add, change, remove or rename components, and provides methods to raise a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanged" /> or <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event.</summary>
	// Token: 0x0200010C RID: 268
	[ComVisible(true)]
	public interface IComponentChangeService
	{
		/// <summary>Occurs when a component has been added.</summary>
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000AA3 RID: 2723
		// (remove) Token: 0x06000AA4 RID: 2724
		event ComponentEventHandler ComponentAdded;

		/// <summary>Occurs when a component is in the process of being added.</summary>
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000AA5 RID: 2725
		// (remove) Token: 0x06000AA6 RID: 2726
		event ComponentEventHandler ComponentAdding;

		/// <summary>Occurs when a component has been changed.</summary>
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000AA7 RID: 2727
		// (remove) Token: 0x06000AA8 RID: 2728
		event ComponentChangedEventHandler ComponentChanged;

		/// <summary>Occurs when a component is in the process of being changed.</summary>
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000AA9 RID: 2729
		// (remove) Token: 0x06000AAA RID: 2730
		event ComponentChangingEventHandler ComponentChanging;

		/// <summary>Occurs when a component has been removed.</summary>
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000AAB RID: 2731
		// (remove) Token: 0x06000AAC RID: 2732
		event ComponentEventHandler ComponentRemoved;

		/// <summary>Occurs when a component is in the process of being removed.</summary>
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000AAD RID: 2733
		// (remove) Token: 0x06000AAE RID: 2734
		event ComponentEventHandler ComponentRemoving;

		/// <summary>Occurs when a component is renamed.</summary>
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000AAF RID: 2735
		// (remove) Token: 0x06000AB0 RID: 2736
		event ComponentRenameEventHandler ComponentRename;

		/// <summary>Announces to the component change service that a particular component has changed.</summary>
		/// <param name="component">The component that has changed. </param>
		/// <param name="member">The member that has changed. This is null if this change is not related to a single member. </param>
		/// <param name="oldValue">The old value of the member. This is valid only if the member is not null. </param>
		/// <param name="newValue">The new value of the member. This is valid only if the member is not null. </param>
		// Token: 0x06000AB1 RID: 2737
		void OnComponentChanged(object component, MemberDescriptor member, object oldValue, object newValue);

		/// <summary>Announces to the component change service that a particular component is changing.</summary>
		/// <param name="component">The component that is about to change. </param>
		/// <param name="member">The member that is changing. This is null if this change is not related to a single member. </param>
		// Token: 0x06000AB2 RID: 2738
		void OnComponentChanging(object component, MemberDescriptor member);
	}
}
