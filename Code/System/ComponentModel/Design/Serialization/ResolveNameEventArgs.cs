using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.ResolveName" /> event.</summary>
	// Token: 0x02000136 RID: 310
	public class ResolveNameEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.ResolveNameEventArgs" /> class.</summary>
		/// <param name="name">The name to resolve. </param>
		// Token: 0x06000BA1 RID: 2977 RVA: 0x0001E680 File Offset: 0x0001C880
		public ResolveNameEventArgs(string name)
		{
			this.name = name;
		}

		/// <summary>Gets the name of the object to resolve.</summary>
		/// <returns>The name of the object to resolve.</returns>
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0001E690 File Offset: 0x0001C890
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets or sets the object that matches the name.</summary>
		/// <returns>The object that the name is associated with.</returns>
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0001E698 File Offset: 0x0001C898
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x0001E6A0 File Offset: 0x0001C8A0
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x0400030A RID: 778
		private string name;

		// Token: 0x0400030B RID: 779
		private object value;
	}
}
