using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.BindingSource.AddingNew" /> event.</summary>
	// Token: 0x020000C3 RID: 195
	public class AddingNewEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AddingNewEventArgs" /> class using no parameters.</summary>
		// Token: 0x0600087C RID: 2172 RVA: 0x00019628 File Offset: 0x00017828
		public AddingNewEventArgs() : this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AddingNewEventArgs" /> class using the specified object as the new item.</summary>
		/// <param name="newObject">An <see cref="T:System.Object" /> to use as the new item value.</param>
		// Token: 0x0600087D RID: 2173 RVA: 0x00019634 File Offset: 0x00017834
		public AddingNewEventArgs(object newObject)
		{
			this.obj = newObject;
		}

		/// <summary>Gets or sets the object to be added to the binding list. </summary>
		/// <returns>The <see cref="T:System.Object" /> to be added as a new item to the associated collection. </returns>
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x00019644 File Offset: 0x00017844
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x0001964C File Offset: 0x0001784C
		public object NewObject
		{
			get
			{
				return this.obj;
			}
			set
			{
				this.obj = value;
			}
		}

		// Token: 0x04000236 RID: 566
		private object obj;
	}
}
