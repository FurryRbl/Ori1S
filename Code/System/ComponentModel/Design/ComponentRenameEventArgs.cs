using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRename" /> event.</summary>
	// Token: 0x020000F9 RID: 249
	[ComVisible(true)]
	public class ComponentRenameEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentRenameEventArgs" /> class.</summary>
		/// <param name="component">The component to be renamed. </param>
		/// <param name="oldName">The old name of the component. </param>
		/// <param name="newName">The new name of the component. </param>
		// Token: 0x06000A1C RID: 2588 RVA: 0x0001CCE4 File Offset: 0x0001AEE4
		public ComponentRenameEventArgs(object component, string oldName, string newName)
		{
			this.component = component;
			this.oldName = oldName;
			this.newName = newName;
		}

		/// <summary>Gets the component that is being renamed.</summary>
		/// <returns>The component that is being renamed.</returns>
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0001CD04 File Offset: 0x0001AF04
		public object Component
		{
			get
			{
				return this.component;
			}
		}

		/// <summary>Gets the name of the component after the rename event.</summary>
		/// <returns>The name of the component after the rename event.</returns>
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0001CD0C File Offset: 0x0001AF0C
		public virtual string NewName
		{
			get
			{
				return this.newName;
			}
		}

		/// <summary>Gets the name of the component before the rename event.</summary>
		/// <returns>The previous name of the component.</returns>
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x0001CD14 File Offset: 0x0001AF14
		public virtual string OldName
		{
			get
			{
				return this.oldName;
			}
		}

		// Token: 0x040002B4 RID: 692
		private object component;

		// Token: 0x040002B5 RID: 693
		private string oldName;

		// Token: 0x040002B6 RID: 694
		private string newName;
	}
}
