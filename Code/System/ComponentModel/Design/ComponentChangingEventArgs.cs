using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event. This class cannot be inherited.</summary>
	// Token: 0x020000F7 RID: 247
	[ComVisible(true)]
	public sealed class ComponentChangingEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentChangingEventArgs" /> class.</summary>
		/// <param name="component">The component that is about to be changed. </param>
		/// <param name="member">A <see cref="T:System.ComponentModel.MemberDescriptor" /> indicating the member of the component that is about to be changed. </param>
		// Token: 0x06000A17 RID: 2583 RVA: 0x0001CCA4 File Offset: 0x0001AEA4
		public ComponentChangingEventArgs(object component, MemberDescriptor member)
		{
			this.component = component;
			this.member = member;
		}

		/// <summary>Gets the component that is about to be changed or the component that is the parent container of the member that is about to be changed.</summary>
		/// <returns>The component that is about to have a member changed.</returns>
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0001CCBC File Offset: 0x0001AEBC
		public object Component
		{
			get
			{
				return this.component;
			}
		}

		/// <summary>Gets the member that is about to be changed.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.MemberDescriptor" /> indicating the member that is about to be changed, if known, or null otherwise.</returns>
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0001CCC4 File Offset: 0x0001AEC4
		public MemberDescriptor Member
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x040002B1 RID: 689
		private object component;

		// Token: 0x040002B2 RID: 690
		private MemberDescriptor member;
	}
}
