using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentAdded" />, <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentAdding" />, <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRemoved" />, and <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRemoving" /> events.</summary>
	// Token: 0x020000F8 RID: 248
	[ComVisible(true)]
	public class ComponentEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentEventArgs" /> class.</summary>
		/// <param name="component">The component that is the source of the event. </param>
		// Token: 0x06000A1A RID: 2586 RVA: 0x0001CCCC File Offset: 0x0001AECC
		public ComponentEventArgs(IComponent component)
		{
			this.icomp = component;
		}

		/// <summary>Gets the component associated with the event.</summary>
		/// <returns>The component associated with the event.</returns>
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x0001CCDC File Offset: 0x0001AEDC
		public virtual IComponent Component
		{
			get
			{
				return this.icomp;
			}
		}

		// Token: 0x040002B3 RID: 691
		private IComponent icomp;
	}
}
