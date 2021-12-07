using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.TypeDescriptor.Refreshed" /> event.</summary>
	// Token: 0x020001A0 RID: 416
	public class RefreshEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RefreshEventArgs" /> class with the component that has changed.</summary>
		/// <param name="componentChanged">The component that changed. </param>
		// Token: 0x06000EB4 RID: 3764 RVA: 0x000263D4 File Offset: 0x000245D4
		public RefreshEventArgs(object componentChanged)
		{
			if (componentChanged == null)
			{
				throw new ArgumentNullException("componentChanged");
			}
			this.component = componentChanged;
			this.type = this.component.GetType();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RefreshEventArgs" /> class with the type of component that has changed.</summary>
		/// <param name="typeChanged">The <see cref="T:System.Type" /> that changed. </param>
		// Token: 0x06000EB5 RID: 3765 RVA: 0x00026408 File Offset: 0x00024608
		public RefreshEventArgs(Type typeChanged)
		{
			this.type = typeChanged;
		}

		/// <summary>Gets the component that changed its properties, events, or extenders.</summary>
		/// <returns>The component that changed its properties, events, or extenders, or null if all components of the same type have changed.</returns>
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x00026418 File Offset: 0x00024618
		public object ComponentChanged
		{
			get
			{
				return this.component;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> that changed its properties or events.</summary>
		/// <returns>The <see cref="T:System.Type" /> that changed its properties or events.</returns>
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00026420 File Offset: 0x00024620
		public Type TypeChanged
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04000424 RID: 1060
		private object component;

		// Token: 0x04000425 RID: 1061
		private Type type;
	}
}
