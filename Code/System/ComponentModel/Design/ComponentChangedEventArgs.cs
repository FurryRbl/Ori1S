using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanged" /> event. This class cannot be inherited.</summary>
	// Token: 0x020000F6 RID: 246
	[ComVisible(true)]
	public sealed class ComponentChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentChangedEventArgs" /> class.</summary>
		/// <param name="component">The component that was changed. </param>
		/// <param name="member">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that represents the member that was changed. </param>
		/// <param name="oldValue">The old value of the changed member. </param>
		/// <param name="newValue">The new value of the changed member. </param>
		// Token: 0x06000A12 RID: 2578 RVA: 0x0001CC5C File Offset: 0x0001AE5C
		public ComponentChangedEventArgs(object component, MemberDescriptor member, object oldValue, object newValue)
		{
			this.component = component;
			this.member = member;
			this.oldValue = oldValue;
			this.newValue = newValue;
		}

		/// <summary>Gets the component that was modified.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the component that was modified.</returns>
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0001CC84 File Offset: 0x0001AE84
		public object Component
		{
			get
			{
				return this.component;
			}
		}

		/// <summary>Gets the member that has been changed.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.MemberDescriptor" /> that indicates the member that has been changed.</returns>
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0001CC8C File Offset: 0x0001AE8C
		public MemberDescriptor Member
		{
			get
			{
				return this.member;
			}
		}

		/// <summary>Gets the new value of the changed member.</summary>
		/// <returns>The new value of the changed member. This property can be null.</returns>
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0001CC94 File Offset: 0x0001AE94
		public object NewValue
		{
			get
			{
				return this.oldValue;
			}
		}

		/// <summary>Gets the old value of the changed member.</summary>
		/// <returns>The old value of the changed member. This property can be null.</returns>
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0001CC9C File Offset: 0x0001AE9C
		public object OldValue
		{
			get
			{
				return this.newValue;
			}
		}

		// Token: 0x040002AD RID: 685
		private object component;

		// Token: 0x040002AE RID: 686
		private MemberDescriptor member;

		// Token: 0x040002AF RID: 687
		private object oldValue;

		// Token: 0x040002B0 RID: 688
		private object newValue;
	}
}
