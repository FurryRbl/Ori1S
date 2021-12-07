using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.Data.DataColumnCollection.CollectionChanged" /> event.</summary>
	// Token: 0x020000D9 RID: 217
	public class CollectionChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.ComponentModel.CollectionChangeAction" /> values that specifies how the collection changed. </param>
		/// <param name="element">An <see cref="T:System.Object" /> that specifies the instance of the collection where the change occurred. </param>
		// Token: 0x06000951 RID: 2385 RVA: 0x0001B370 File Offset: 0x00019570
		public CollectionChangeEventArgs(CollectionChangeAction action, object element)
		{
			this.changeAction = action;
			this.theElement = element;
		}

		/// <summary>Gets an action that specifies how the collection changed.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.CollectionChangeAction" /> values.</returns>
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x0001B388 File Offset: 0x00019588
		public virtual CollectionChangeAction Action
		{
			get
			{
				return this.changeAction;
			}
		}

		/// <summary>Gets the instance of the collection with the change.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the instance of the collection with the change, or null if you refresh the collection.</returns>
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0001B390 File Offset: 0x00019590
		public virtual object Element
		{
			get
			{
				return this.theElement;
			}
		}

		// Token: 0x0400027D RID: 637
		private CollectionChangeAction changeAction;

		// Token: 0x0400027E RID: 638
		private object theElement;
	}
}
